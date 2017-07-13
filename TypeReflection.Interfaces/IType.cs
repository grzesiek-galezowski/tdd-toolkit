using System;
using System.Collections.Generic;
using TddEbook.TddToolkit.CommonTypes;

namespace TypeReflection.Interfaces
{
  public interface IType
  {
    bool HasPublicParameterlessConstructor();
    bool IsImplementationOfOpenGeneric(Type openGenericType);
    bool IsConcrete();
    IEnumerable<IFieldWrapper> GetAllInstanceFields();
    IEnumerable<IFieldWrapper> GetAllStaticFields();
    IEnumerable<IFieldWrapper> GetAllConstants();
    IEnumerable<IPropertyWrapper> GetAllPublicInstanceProperties();
    Maybe<IConstructorWrapper> PickConstructorWithLeastNonPointersParameters();
    IBinaryOperator Equality();
    IBinaryOperator Inequality();
    bool IsInterface();
    IEnumerable<IEventWrapper> GetAllNonPublicEventsWithoutExplicitlyImplemented();
    IEnumerable<IConstructorWrapper> GetAllPublicConstructors();
    IEnumerable<IFieldWrapper> GetAllPublicInstanceFields();
    IEnumerable<IPropertyWrapper> GetPublicInstanceWritableProperties();
    IEnumerable<IMethod> GetAllPublicInstanceMethodsWithReturnValue();
    bool HasConstructorWithParameters();
    bool CanBeAssignedNullValue();
    Type ToClrType();
    bool IsException();
    bool HasPublicConstructorCountOfAtMost(int i);
    bool IsOpenGeneric(Type type);
  }
}