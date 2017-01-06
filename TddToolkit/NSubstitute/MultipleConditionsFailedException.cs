using System;
using System.Collections.Generic;

namespace TddEbook.TddToolkit.NSubstitute
{
  public class MultipleConditionsFailedException : Exception
  {
    public Dictionary<int, Exception> Exceptions { get; }

    public MultipleConditionsFailedException(Dictionary<int, Exception> exceptions)
      : base(exceptions.Count + " condition(s) failed:" +
             Environment.NewLine + FormatExceptions(exceptions))
    {
      Exceptions = exceptions;
    }

    private static string FormatExceptions(Dictionary<int, Exception> exceptions)
    {
      var result = string.Empty;
      foreach (var exceptionEntry in exceptions)
      {
        result += $"=== FAILED CONDITION {exceptionEntry.Key} ===" + Environment.NewLine;
        result += exceptionEntry.Value + Environment.NewLine;
      }
      return result;
    }
  }
}