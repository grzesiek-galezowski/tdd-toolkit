using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  public class BoooooomException : Exception
  {
      public BoooooomException(string name) 
      : base("Method " + name + " is a part of exploding fake, meaning it should not be called")
      {
        
      }
  }
}

