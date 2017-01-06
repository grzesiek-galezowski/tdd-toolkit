using System;
using NSubstitute.Core;
using NSubstitute.Core.Arguments;

namespace TddEbook.TddToolkit.NSubstitute.ImplementationDetails
{
  public class LambdaArgumentMatcher<T> : IArgumentMatcher, IDescribeNonMatches
  {
    private readonly Action<T>[] _assertionActions;

    public LambdaArgumentMatcher(Action<T>[] assertionActions)
    {
      _assertionActions = assertionActions;
    }

    public bool IsSatisfiedBy(object argument)
    {
      try
      {
        MultipleConditionsExecutionLoop.Execute(_assertionActions, (T)argument);
        return true;
      }
      catch (Exception e)
      {
        return false;
      }
    }

    public string DescribeFor(object argument)
    {
      try
      {
        MultipleConditionsExecutionLoop.Execute(_assertionActions, (T)argument);
        return string.Empty;
      }
      catch (Exception e)
      {
        return e.Message;
      }
    }
  }
}