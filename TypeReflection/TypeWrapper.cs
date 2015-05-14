using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TddEbook.TypeReflection.ImplementationDetails;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection
{
  public interface ITypeWrapper
  {
    bool HasPublicParameterlessConstructor();
    bool IsImplementationOfOpenGeneric(Type openGenericType);
    bool IsConcrete();
    IEnumerable<IFieldWrapper> GetAllInstanceFields();
    IEnumerable<IFieldWrapper> GetAllStaticFields();
    IEnumerable<IPropertyWrapper> GetAllPublicInstanceProperties();
    IConstructorWrapper PickConstructorWithLeastNonPointersParameters();
    IBinaryOperator Equality();
    IBinaryOperator Inequality();
    bool IsInterface();
    IEnumerable<IEventWrapper> GetAllNonPublicEventsWithoutExplicitlyImplemented();
    IEnumerable<IConstructorWrapper> GetAllPublicConstructors();
    IEnumerable<IFieldWrapper> GetAllPublicInstanceFields();
    IEnumerable<IPropertyWrapper> GetPublicInstanceWritableProperties();
    IEnumerable<IMethodWrapper> GetAllPublicInstanceMethodsWithReturnValue();
    bool HasConstructorWithParameters();
    bool CanBeAssignedNullValue();
    Type ToClrType();
  }

  public interface IMethodWrapper
  {
    object InvokeWithAnyArgsOn(object instance, Func<Type, object> valueFactory);
    object GenerateAnyReturnValue(Func<Type, object> valueFactory);
  }

  public class TypeWrapper : ITypeWrapper
  {
    private readonly Type _type;

    public TypeWrapper(Type type)
    {
      _type = type;
    }

    public bool HasPublicParameterlessConstructor()
    {
      return GetPublicParameterlessConstructorInfo() != null;
    }

    private bool HasNonPublicParameterlessConstructor()
    {
      return GetNonPublicParameterlessConstructorInfo() != null;
    }

    private ConstructorInfo GetNonPublicParameterlessConstructorInfo()
    {
      return _type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
    }

    private ConstructorInfo GetPublicParameterlessConstructorInfo()
    {
      return _type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);
    }

    public bool IsImplementationOfOpenGeneric(Type openGenericType)
    {
      return _type.GetInterfaces().Any(
        ifaceType => ifaceType.IsGenericType && ifaceType.GetGenericTypeDefinition() == openGenericType);
    }

    public bool IsConcrete()
    {
      return !_type.IsAbstract && !_type.IsInterface;
    }

    public IEnumerable<IFieldWrapper> GetAllInstanceFields()
    {
      var fields = _type.GetFields(
        BindingFlags.Instance 
        | BindingFlags.Public 
        | BindingFlags.NonPublic);
      return fields.Select(f => new FieldWrapper(f));
    }

    public IEnumerable<IFieldWrapper> GetAllStaticFields()
    {
      return GetAllFields(_type).Where(fieldInfo =>
                                       fieldInfo.IsStatic &&
                                       !IsConst(fieldInfo) &&
                                       !IsCompilerGenerated(fieldInfo) &&
                                       !IsDelegate(fieldInfo.FieldType))
                                .Select(f => new FieldWrapper(f));
    }

    public IEnumerable<IPropertyWrapper> GetAllPublicInstanceProperties()
    {
      var properties = _type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
      return properties.Select(p => new PropertyWrapper(p));
    }

    public IConstructorWrapper PickConstructorWithLeastNonPointersParameters()
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

      return leastParamsConstructor;
    }

    private const string OpEquality = "op_Equality";
    private const string OpInequality = "op_Inequality";

    private Maybe<MethodInfo> EqualityMethod()
    {
      var equality = _type.GetMethod(OpEquality);

      return equality == null ? Maybe<MethodInfo>.Nothing : new Maybe<MethodInfo>(equality);
    }

    private Maybe<MethodInfo> InequalityMethod()
    {
      var inequality = _type.GetMethod(OpInequality);

      return inequality == null ? Maybe<MethodInfo>.Nothing : new Maybe<MethodInfo>(inequality);
    }

    private Maybe<MethodInfo> ValueTypeEqualityMethod()
    {
      return _type.IsValueType ?
               Maybe.Wrap(GetType().GetMethod("ValuesEqual"))
               : Maybe<MethodInfo>.Nothing;

    }

    private Maybe<MethodInfo> ValueTypeInequalityMethod()
    {
      return _type.IsValueType ?
               Maybe.Wrap(GetType().GetMethod("ValuesNotEqual")) 
               : Maybe<MethodInfo>.Nothing;
    }

    public IBinaryOperator Equality()
    {
      return BinaryOperator.Wrap(EqualityMethod(), ValueTypeEqualityMethod(), "operator ==");
    }

    public IBinaryOperator Inequality()
    {
      return BinaryOperator.Wrap(InequalityMethod(), ValueTypeInequalityMethod(), "operator !=");
    }

    public static ITypeWrapper For(Type type)
    {
      return new TypeWrapper(type);
    }

    public static ITypeWrapper For(object obj)
    {
      return new TypeWrapper(obj.GetType());
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
      return _type.IsInterface;
    }

    private static bool IsCompilerGenerated(FieldInfo fieldInfo)
    {
      return fieldInfo.FieldType.IsDefined(typeof(CompilerGeneratedAttribute), false);
    }

    private static bool IsConst(FieldInfo fieldInfo)
    {
      return fieldInfo.IsLiteral && !fieldInfo.IsInitOnly;
    }

    private static IEnumerable<FieldInfo> GetAllFields(Type type)
    {
      return type.GetNestedTypes().SelectMany(GetAllFields)
                 .Concat(type.GetFields(
                   BindingFlags.Public 
                   | BindingFlags.NonPublic 
                   | BindingFlags.Static
                   | BindingFlags.DeclaredOnly));
    }

    private static bool IsDelegate(Type type)
    {
      return typeof(MulticastDelegate).IsAssignableFrom(type.BaseType);
    }

    public IEnumerable<IEventWrapper> GetAllNonPublicEventsWithoutExplicitlyImplemented()
    {
      return _type.GetEvents(
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
        var interfaces = eventDeclaringType.GetInterfaces();
        foreach (var @interface in interfaces)
        {
          var methodsImplementedInInterface = eventDeclaringType.GetInterfaceMap(@interface).TargetMethods;
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
      var publicConstructors = TryToObtainPublicConstructors();

      if (publicConstructors.Any())
      {
        return publicConstructors;
      }
      else if (HasPublicParameterlessConstructor() || _type.IsPrimitive || _type.IsAbstract)
      {
        return new List<IConstructorWrapper> {new DefaultParameterlessConstructor(GetPublicParameterlessConstructorInfo())};
      }
      else
      {
        return GetAllInternalConstructors();
      }
    }

    private IEnumerable<IConstructorWrapper> GetAllInternalConstructors()
    {
      var internalConstructors = TryToObtainInternalConstructors();

      if (internalConstructors.Any())
      {
        return internalConstructors;
      }
      else if (HasNonPublicParameterlessConstructor())
      {
        return new List<IConstructorWrapper>
        {
          new DefaultParameterlessConstructor(GetNonPublicParameterlessConstructorInfo())
        };
      }
      else
      {
        return new List<IConstructorWrapper>();
      }
    }

    private List<IConstructorWrapper> TryToObtainInternalConstructors()
    {
      return _type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Where(c => c.IsAssembly)
        .Select(c => (IConstructorWrapper)(new ConstructorWrapper(c))).ToList();
    }

    private List<ConstructorWrapper> TryToObtainPublicConstructors()
    {
      return _type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
        .Select(c => new ConstructorWrapper(c)).ToList();
    }

    public IEnumerable<IFieldWrapper> GetAllPublicInstanceFields()
    {
      return _type.GetFields(BindingFlags.Public | BindingFlags.Instance).Select(f => new FieldWrapper(f));
    }

    public IEnumerable<IPropertyWrapper> GetPublicInstanceWritableProperties()
    {
      return _type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite).Select(p => new PropertyWrapper(p));
    }

    public IEnumerable<IMethodWrapper> GetAllPublicInstanceMethodsWithReturnValue()
    {
      return _type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.ReturnType != typeof(void)).
        Select(p => new MethodWrapper(p));
    }
    //TODO even strict mocks can be done this way...

    public bool HasConstructorWithParameters()
    {
      return _type.IsPrimitive;
    }

    public bool CanBeAssignedNullValue()
    {
      return !_type.IsValueType && !_type.IsPrimitive;
    }

    public Type ToClrType()
    {
      return _type; //todo at the very end, this should be removed
    }
  }
}