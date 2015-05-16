using System;
using TddEbook.TypeReflection.ImplementationDetails;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection
{
  public static class TypeOf<T>
  {
    private static readonly ITypeWrapper _typeWrapper;

    static TypeOf()
    {
      _typeWrapper = TypeWrapper.For(typeof (T));
    }

    public static bool HasParameterlessConstructor()
    {
      return _typeWrapper.HasPublicParameterlessConstructor();
    }

    public static bool IsImplementationOfOpenGeneric(Type openGenericType)
    {
      return _typeWrapper.IsImplementationOfOpenGeneric(openGenericType);
    }

    public static bool IsConcrete()
    {
      return _typeWrapper.IsConcrete();
    }

    public static IBinaryOperator<T, bool> Equality()
    {

      return BinaryOperator<T, bool>.Wrap(_typeWrapper.Equality());
    }

    public static IBinaryOperator<T, bool> Inequality()
    {
      return BinaryOperator<T, bool>.Wrap(_typeWrapper.Inequality());
    }

    public static bool Is<T1>()
    {
      return typeof (T) == typeof (T1);
    }
  }
}