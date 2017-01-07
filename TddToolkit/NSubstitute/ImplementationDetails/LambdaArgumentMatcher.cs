using System;
using System.Linq;
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
        ExecuteAssertionsFor(argument);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    private void ExecuteAssertionsFor(object argument)
    {
      if (_assertionActions.Length == 1)
      {
        _assertionActions.First()((T) argument);
      }
      else
      {
        MultipleConditionsExecutionLoop.Execute(_assertionActions, (T) argument);
      }
    }

    public string DescribeFor(object argument)
    {
      try
      {
        ExecuteAssertionsFor(argument);
        return string.Empty;
      }
      catch (Exception e)
      {
        return e.Message;
      }
    }
  }
}