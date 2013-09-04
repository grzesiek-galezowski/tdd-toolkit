using System;

namespace TddEbook.TddToolkit
{
  public class AlikeException : Exception
  {
    public AlikeException(string differencesString) : base(differencesString)
    {
      
    }
  }
}