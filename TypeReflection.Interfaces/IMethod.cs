using System;

namespace TypeReflection.Interfaces
{
  public interface IMethod
  {
    object InvokeWithAnyArgsOn(object instance, Func<Type, object> valueFactory);
    object GenerateAnyReturnValue(Func<Type, object> valueFactory);
  }
}