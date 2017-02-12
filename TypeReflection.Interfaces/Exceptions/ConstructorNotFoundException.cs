using System;

namespace TypeReflection.Interfaces.Exceptions
{
  public class ConstructorNotFoundException : Exception
  {
    public ConstructorNotFoundException(string s)
      : base(s)
    {
      
    }
  }
}