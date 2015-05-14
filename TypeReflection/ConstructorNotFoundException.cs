using System;

namespace TddEbook.TypeReflection
{
  public class ConstructorNotFoundException : Exception
  {
    public ConstructorNotFoundException(string s)
      : base(s)
    {
      
    }
  }
}