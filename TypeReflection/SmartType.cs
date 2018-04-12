using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TddEbook.TddToolkit.CommonTypes;
using TypeReflection.ImplementationDetails;
using TypeReflection.ImplementationDetails.ConstructorRetrievals;
using TypeReflection.Interfaces;
using static TypeReflection.ImplementationDetails.ConstructorWrapper;

namespace TddEbook.TypeReflection
{
  public interface ISmartType : IType, IConstructorQueries
  {
  }

  public class SmartType : ISmartType
  {
    private readonly Type _type;
    private readonly ConstructorRetrieval _constructorRetrieval;
    private readonly TypeInfo _typeInfo;

    public SmartType(Type type, ConstructorRetrieval constructorRetrieval)
    {
      _type = type;
      _constructorRetrieval = constructorRetrieval;
      _typeInfo = _type.GetTypeInfo();
    }

    public bool HasPublicParameterlessConstructor()
    {
      return GetPublicParameterlessConstructor().HasValue || _typeInfo.IsPrimitive || _typeInfo.IsAbstract;
    }

    public Maybe<IConstructorWrapper> GetNonPublicParameterlessConstructorInfo()
    {
      var constructorInfo = _type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
      if (constructorInfo != null)
      {
        return Maybe.Wrap(DefaultParameterlessConstructor.ForOrdinaryType(constructorInfo));
      }
      else
      {
        return Maybe<IConstructorWrapper>.Not;
      }
    }

    public Maybe<IConstructorWrapper> GetPublicParameterlessConstructor()
    {

      var constructorInfo = _type.GetConstructor(
        BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);
      if (constructorInfo == null)
      {
        return Maybe<IConstructorWrapper>.Not;
      }
      else
      {
        return Maybe.Wrap(DefaultParameterlessConstructor.ForOrdinaryType(constructorInfo));
      }
    }

    public bool IsImplementationOfOpenGeneric(Type openGenericType)
    {
      return _typeInfo.GetInterfaces().Any(
        ifaceType => IsOpenGeneric(ifaceType, openGenericType));
    }

    public bool IsOpenGeneric(Type openGenericType)
    {
      return IsOpenGeneric(_typeInfo, openGenericType);
    }

    private static bool IsOpenGeneric(Type checkedType, Type openGenericType)
    {
      return checkedType.GetTypeInfo().IsGenericType && 
             checkedType.GetGenericTypeDefinition() == openGenericType;
    }

    public bool IsConcrete()
    {
      return !_typeInfo.IsAbstract && !_typeInfo.IsInterface;
    }

    public IEnumerable<IFieldWrapper> GetAllInstanceFields()
    {
      var fields = _typeInfo.GetFields(
        BindingFlags.Instance 
        | BindingFlags.Public 
        | BindingFlags.NonPublic);
      return fields.Select(f => new FieldWrapper(f));
    }

    public IEnumerable<IFieldWrapper> GetAllStaticFields()
    {
      //bug first convert to field wrappers and then ask questions, not the other way round.
      //bug GetAllFields() should return field wrappers
      return GetAllFields(_type).Where(fieldInfo =>
                                       fieldInfo.IsStatic &&
                                       !new FieldWrapper(fieldInfo).IsConstant() &&
                                       !IsCompilerGenerated(fieldInfo) &&
                                       !IsDelegate(fieldInfo.FieldType))
                                .Select(f => new FieldWrapper(f));
    }

    public IEnumerable<IFieldWrapper> GetAllConstants()
    {
      return GetAllFields(_type).Select(f => new FieldWrapper(f)).Where(f => f.IsConstant());
    }

    public IEnumerable<IPropertyWrapper> GetAllPublicInstanceProperties()
    {
      var properties = _typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public);
      return properties.Select(p => new PropertyWrapper(p));
    }

    public Maybe<IConstructorWrapper> PickConstructorWithLeastNonPointersParameters()
    {
      IConstructorWrapper leastParamsConstructor = null;

      var constructors = For(_type).GetAllPublicConstructors();
      var numberOfParams = int.MaxValue;

      foreach (var typeConstructor in constructors)
      {
        if (
          typeConstructor.HasNonPointerArgumentsOnly()
          && typeConstructor.HasLessParametersThan(numberOfParams))
        {
          leastParamsConstructor = typeConstructor;
          numberOfParams = typeConstructor.GetParametersCount();
        }
      }

      return Maybe.Wrap(leastParamsConstructor);
    }

    private const string OpEquality = "op_Equality";
    private const string OpInequality = "op_Inequality";

    private Maybe<MethodInfo> EqualityMethod()
    {
      var equality = _typeInfo.GetMethod(OpEquality);

      return equality == null ? Maybe<MethodInfo>.Not : new Maybe<MethodInfo>(equality);
    }

    private Maybe<MethodInfo> InequalityMethod()
    {
      var inequality = _typeInfo.GetMethod(OpInequality);

      return inequality == null ? Maybe<MethodInfo>.Not : new Maybe<MethodInfo>(inequality);
    }

    private Maybe<MethodInfo> ValueTypeEqualityMethod()
    {
      return _typeInfo.IsValueType ?
               Maybe.Wrap(GetType().GetTypeInfo().GetMethod("ValuesEqual"))
               : Maybe<MethodInfo>.Not;

    }

    private Maybe<MethodInfo> ValueTypeInequalityMethod()
    {
      return _typeInfo.IsValueType ?
               Maybe.Wrap(GetType().GetTypeInfo().GetMethod("ValuesNotEqual")) 
               : Maybe<MethodInfo>.Not;
    }

    public IBinaryOperator Equality()
    {
      return BinaryOperator.Wrap(EqualityMethod(), ValueTypeEqualityMethod(), "operator ==");
    }

    public IBinaryOperator Inequality()
    {
      return BinaryOperator.Wrap(InequalityMethod(), ValueTypeInequalityMethod(), "operator !=");
    }

    public static ISmartType For(Type type)
    {
      return new SmartType(type, new ConstructorRetrievalFactory().Create());
    }

    public static ISmartType ForTypeOf(object obj)
    {
      return new SmartType(obj.GetType(), new ConstructorRetrievalFactory().Create());
    }

    public static bool ValuesEqual(object instance1, object instance2)
    {
      return Equals(instance1, instance2);
    }

    public static bool ValuesNotEqual(object instance1, object instance2)
    {
      return !Equals(instance1, instance2);
    }

    public bool IsInterface()
    {
      return _typeInfo.IsInterface;
    }

    private static bool IsCompilerGenerated(FieldInfo fieldInfo) //?? should it be defined on a type?
    {
      return fieldInfo.FieldType.GetTypeInfo().IsDefined(typeof(CompilerGeneratedAttribute), false);
    }

    private static IEnumerable<FieldInfo> GetAllFields(Type type)
    {
      return type.GetTypeInfo().GetNestedTypes().SelectMany(GetAllFields)
                 .Concat(type.GetTypeInfo().GetFields(
                   BindingFlags.Public 
                   | BindingFlags.NonPublic 
                   | BindingFlags.Static
                   | BindingFlags.DeclaredOnly));
    }

    private static bool IsDelegate(Type type)
    {
      return typeof(MulticastDelegate).GetTypeInfo().IsAssignableFrom(
        type.GetTypeInfo().BaseType);
    }

    public IEnumerable<IEventWrapper> GetAllNonPublicEventsWithoutExplicitlyImplemented()
    {
      return _typeInfo.GetEvents(
        BindingFlags.NonPublic 
        | BindingFlags.Instance
        | BindingFlags.DeclaredOnly)
                  .Where(IsNotExplicitlyImplemented)
                  .Select(e => new EventWrapper(e));
    }

    private static bool IsNotExplicitlyImplemented(EventInfo eventInfo)
    {
      var eventDeclaringType = eventInfo.DeclaringType;
      if (eventDeclaringType != null)
      {
        var interfaces = eventDeclaringType.GetTypeInfo().GetInterfaces();
        foreach (var @interface in interfaces)
        {
          var methodsImplementedInInterface = eventDeclaringType
            .GetInterfaceMap(@interface).TargetMethods;
          var addMethod = eventInfo.GetAddMethod(true);
          if (methodsImplementedInInterface.Where(m => m.IsPrivate).Contains(addMethod))
          {
            return false;
          }
        }
      }
      return true;
    }

    public IEnumerable<IConstructorWrapper> GetAllPublicConstructors()
    {
      return _constructorRetrieval.RetrieveFrom(this);
    }

    public List<IConstructorWrapper> TryToObtainInternalConstructorsWithoutRecursiveArguments()
    {
      return TryToObtainInternalConstructors().Where(c => c.IsNotRecursive()).ToList();
    }

    private List<IConstructorWrapper> TryToObtainInternalConstructors()
    {
      var constructorInfos = _typeInfo.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
      var enumerable = constructorInfos.Where(IsInternal);

      var wrappers = enumerable.Select(c => (IConstructorWrapper) (FromConstructorInfo(c))).ToList();
      return wrappers;
    }

    public List<ConstructorWrapper> TryToObtainPublicConstructors()
    {
      return _typeInfo.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
        .Select(c => FromConstructorInfo(c)).ToList();
    }

    public IEnumerable<IConstructorWrapper> TryToObtainPublicConstructorsWithoutRecursiveArguments()
    {
      return TryToObtainPublicConstructors().Where(c => c.IsNotRecursive());
    }

    public IEnumerable<IConstructorWrapper> TryToObtainPublicConstructorsWithRecursiveArguments()
    {
      return TryToObtainPublicConstructors().Where(c => c.IsRecursive());
    }

    public IEnumerable<IConstructorWrapper> TryToObtainInternalConstructorsWithRecursiveArguments()
    {
      return TryToObtainInternalConstructors().Where(c => c.IsRecursive()).ToList();
    }

    public IEnumerable<IConstructorWrapper> TryToObtainPrimitiveTypeConstructor()
    {
      return DefaultParameterlessConstructor.ForValue(_type);
    }

    public IEnumerable<IConstructorWrapper> TryToObtainPublicStaticFactoryMethodWithoutRecursion()
    {
      return _typeInfo.GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Where(m => !m.IsSpecialName)
        .Where(IsNotImplicitCast)
        .Where(IsNotExplicitCast)
        .Select(FromStaticMethodInfo)
        .Where(c => c.IsFactoryMethod());
    }

    public IEnumerable<IFieldWrapper> GetAllPublicInstanceFields()
    {
      return _typeInfo.GetFields(
        BindingFlags.Public | BindingFlags.Instance).Select(f => new FieldWrapper(f));
    }

    public IEnumerable<IPropertyWrapper> GetPublicInstanceWritableProperties()
    {
      return _typeInfo.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.CanWrite)
        .Select(p => new PropertyWrapper(p));
    }

    public IEnumerable<IMethod> GetAllPublicInstanceMethodsWithReturnValue()
    {
      return _typeInfo.GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.ReturnType != typeof(void)).
        Select(p => new SmartMethod(p));
    }
    //TODO even strict mocks can be done this way...

    public bool HasConstructorWithParameters()
    {
      return _typeInfo.IsPrimitive;
    }

    public bool CanBeAssignedNullValue()
    {
      return !_typeInfo.IsValueType && !_typeInfo.IsPrimitive;
    }

    public Type ToClrType()
    {
      return _type; //todo at the very end, this should be removed
    }

    public bool IsException()
    {
      return _type == typeof(Exception) ||
        _typeInfo.IsSubclassOf(typeof(Exception));
    }

    public bool HasPublicConstructorCountOfAtMost(int i)
    {
      return GetAllPublicConstructors().Count() <= i;
    }

    private static bool IsNotExplicitCast(MethodInfo mi)
    {
      return !string.Equals(mi.Name, "op_Explicit", StringComparison.Ordinal);
    }

    private static bool IsNotImplicitCast(MethodInfo mi)
    {
      return !string.Equals(mi.Name, "op_Implicit", StringComparison.Ordinal);
    }
  }

}