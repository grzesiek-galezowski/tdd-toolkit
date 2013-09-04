using System;

namespace TddEbook.TddToolkit
{
  public class NotAlikeException : Exception
  {
    public NotAlikeException(string differences) : base(differences)
    {
      
    }
  }
}