using System;
using System.Collections.Generic;
using System.Linq;

namespace TddEbook.TddToolkit.NSubstitute.ImplementationDetails
{
  internal static class MultipleConditionsExecutionLoop
  {
    public static void Execute<T>(IReadOnlyList<Action<T>> assertionActions, T actual)
    {
      var exceptions = new Dictionary<int, Exception>();
      for (var i = 0; i < assertionActions.Count; ++i)
      {
        try
        {
          assertionActions[i].Invoke(actual);
        }
        catch (Exception e)
        {
          exceptions.Add(i + 1, e);
        }
      }
      if (exceptions.Any())
      {
        throw new MultipleConditionsFailedException(exceptions);
      }
    }
  }
}