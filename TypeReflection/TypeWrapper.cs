using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TddEbook.TypeReflection.ImplementationDetails;
using TddEbook.TypeReflection.ImplementationDetails.ConstructorRetrievals;
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

  public interface IConstructorQueries
  {
    bool HasPublicParameterlessConstructor();
    ConstructorInfo GetNonPublicParameterlessConstructorInfo();
    ConstructorInfo GetPublicParameterlessConstructorInfo();
    List<IConstructorWrapper> TryToObtainInternalConstructorsWithoutRecursion();
    List<ConstructorWrapper> TryToObtainPublicConstructors();
  }

  public class TypeWrapper : ITypeWrapper, IConstructorQueries
  {
    private readonly Type _type;

    public TypeWrapper(Type type)
    {
      _type = type;
    }

    public bool HasPublicParameterlessConstructor()
    {
      return GetPublicParameterlessConstructorInfo() != null || _type.IsPrimitive || _type.IsAbstract;
    }

    private bool HasNonPublicParameterlessConstructor()
    {
      return GetNonPublicParameterlessConstructorInfo() != null;
    }

    public ConstructorInfo GetNonPublicParameterlessConstructorInfo()
    {
      return _type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
    }

    public ConstructorInfo GetPublicParameterlessConstructorInfo()
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
      return ConstructorRetrievalFactory.Create().RetrieveFrom(this);
    }


    public List<IConstructorWrapper> TryToObtainInternalConstructorsWithoutRecursion()
    {
      var constructorInfos = _type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
      var enumerable = constructorInfos.Where(c => c.IsAssembly);
      var wrappers = enumerable.Select(c => (IConstructorWrapper)(new ConstructorWrapper(c))).ToList();
      return wrappers;
    }

    public List<ConstructorWrapper> TryToObtainPublicConstructors()
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

  public class MemoizingTypeWrapper : ITypeWrapper
  {
    private readonly ITypeWrapper _typeWrapper;

    private bool? _hasParameterlessConstructor;
    private bool? _isOpenGeneric;
    private bool? _isImplementationOfOpenGeneric;
    private bool? _isConcrete;
    private Maybe<IEnumerable<IFieldWrapper>> _allStaticFields;
    private Maybe<IEnumerable<IFieldWrapper>> _allPublicInstanceFields;
    private Maybe<IEnumerable<IPropertyWrapper>> _allPublicInstanceProperties;
    private Maybe<IEnumerable<IPropertyWrapper>> _allPublicInstanceWritableProperties;
    private Maybe<IConstructorWrapper> _constructorWithLeastNonPointersParameters;
    private Maybe<IBinaryOperator> _equality;
    private Maybe<IBinaryOperator> _inequality;
    private bool? _isInterface;
    private Maybe<IEnumerable<IEventWrapper>> _allNonPublicEventsWithoutExplicitlyImplemented;
    private Maybe<IEnumerable<IConstructorWrapper>> _allPublicConstructors;
    private Maybe<IEnumerable<IFieldWrapper>> _allInstanceFields;

    public MemoizingTypeWrapper(ITypeWrapper typeWrapper)
    {
      _typeWrapper = typeWrapper;
    }

    public bool HasPublicParameterlessConstructor()
    {
      if (!_hasParameterlessConstructor.HasValue)
      {
        _hasParameterlessConstructor = _typeWrapper.HasPublicParameterlessConstructor();
      }
      return _hasParameterlessConstructor.Value;
    }

    public bool IsImplementationOfOpenGeneric(Type openGenericType)
    {
      return _typeWrapper.IsImplementationOfOpenGeneric(openGenericType); //bug deal with it later
    }

    public bool IsConcrete()
    {
      if (!_isConcrete.HasValue)
      {
        _isConcrete = _typeWrapper.IsConcrete();
      }
      return _isConcrete.Value;
    }

    public IEnumerable<IFieldWrapper> GetAllInstanceFields()
    {
      if (!_allInstanceFields.HasValue)
      {
        _allInstanceFields = Maybe.Wrap(_typeWrapper.GetAllInstanceFields());
      }
      return _allInstanceFields.Value;
    }

    public IEnumerable<IFieldWrapper> GetAllStaticFields()
    {
      if (!_allStaticFields.HasValue)
      {
        _allStaticFields = Maybe.Wrap(_typeWrapper.GetAllStaticFields());
      }
      return _allStaticFields.Value;
    }

    public IEnumerable<IPropertyWrapper> GetAllPublicInstanceProperties()
    {
      if (!_allPublicInstanceProperties.HasValue)
      {
        _allPublicInstanceProperties = Maybe.Wrap(_typeWrapper.GetAllPublicInstanceProperties());
      }
      return _allPublicInstanceProperties.Value;
    }

    public IConstructorWrapper PickConstructorWithLeastNonPointersParameters()
    {
      if (!_constructorWithLeastNonPointersParameters.HasValue)
      {
        _constructorWithLeastNonPointersParameters = Maybe.Wrap(_typeWrapper.PickConstructorWithLeastNonPointersParameters());
      }
      return _constructorWithLeastNonPointersParameters.Value;
    }

    public IBinaryOperator Equality()
    {
      if (!_equality.HasValue)
      {
        _equality = Maybe.Wrap(_typeWrapper.Equality());
      }
      return _equality.Value;

    }

    public IBinaryOperator Inequality()
    {
      if (!_inequality.HasValue)
      {
        _inequality = Maybe.Wrap(_typeWrapper.Inequality());
      }
      return _inequality.Value;
    }

    public bool IsInterface()
    {
      if (!_isInterface.HasValue)
      {
        _isInterface = _typeWrapper.IsInterface();
      }
      return _isInterface.Value;

    }

    public IEnumerable<IEventWrapper> GetAllNonPublicEventsWithoutExplicitlyImplemented()
    {
      if (!_allNonPublicEventsWithoutExplicitlyImplemented.HasValue)
      {
        _allNonPublicEventsWithoutExplicitlyImplemented = Maybe.Wrap(_typeWrapper.GetAllNonPublicEventsWithoutExplicitlyImplemented());
      }
      return _allNonPublicEventsWithoutExplicitlyImplemented.Value;
    }

    public IEnumerable<IConstructorWrapper> GetAllPublicConstructors()
    {
      if (!_allPublicConstructors.HasValue)
      {
        _allPublicConstructors = Maybe.Wrap(_typeWrapper.GetAllPublicConstructors());
      }
      return _allPublicConstructors.Value;
    }

    public IEnumerable<IFieldWrapper> GetAllPublicInstanceFields()
    {
      if (!_allPublicInstanceFields.HasValue)
      {
        _allPublicInstanceFields = Maybe.Wrap(_typeWrapper.GetAllPublicInstanceFields());
      }
      return _allPublicInstanceFields.Value;
    }

    public IEnumerable<IPropertyWrapper> GetPublicInstanceWritableProperties()
    {
      if (!_allPublicInstanceWritableProperties.HasValue)
      {
        _allPublicInstanceWritableProperties = Maybe.Wrap(_typeWrapper.GetPublicInstanceWritableProperties());
      }
      return _allPublicInstanceWritableProperties.Value;
    }

    public IEnumerable<IMethodWrapper> GetAllPublicInstanceMethodsWithReturnValue()
    {
      throw new NotImplementedException();
    }

    public bool HasConstructorWithParameters()
    {
      return _typeWrapper.HasConstructorWithParameters();
    }

    public bool CanBeAssignedNullValue()
    {
      throw new NotImplementedException();
    }

    public Type ToClrType()
    {
      throw new NotImplementedException();
    }
  }
}