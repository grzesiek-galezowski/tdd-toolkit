using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TddEbook.TypeReflection.ImplementationDetails;

namespace TddEbook.TypeReflection
{
  public class TypeWrapper
  {
    private readonly Type _type;

    public TypeWrapper(Type type)
    {
      _type = type;
    }

    public bool HasParameterlessConstructor()
    {
      var constructors = ConstructorWrapper.ExtractAllPublicFrom(_type);
      return constructors.Any(c => c.IsParameterless());
    }

    public bool IsOpenGeneric(Type openGenericType)
    {
      return _type.IsGenericType && _type.GetGenericTypeDefinition() == openGenericType;
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
      var constructors = ConstructorWrapper.ExtractAllPublicFrom(_type);
      IConstructorWrapper leastParamsConstructor = null;
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

    public static TypeWrapper For(Type type)
    {
      return new TypeWrapper(type);
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
  }
}