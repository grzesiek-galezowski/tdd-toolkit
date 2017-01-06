using System;
using NSubstitute.Core;
using NSubstitute.Core.Arguments;

namespace TddEbook.TddToolkit.NSubstitute.ImplementationDetails
{
  public class EquivalentArgumentMatcher<T> : IArgumentMatcher, IDescribeNonMatches
  {
    private static readonly ArgumentFormatter DefaultArgumentFormatter = new ArgumentFormatter();
    private readonly object _expected;
    private readonly EquivalencyAssertion _equivalencyAssertion;

    public EquivalentArgumentMatcher(object expected, EquivalencyAssertion equivalencyAssertion)
    {
      _expected = expected;
      _equivalencyAssertion = equivalencyAssertion;
    }

    public override string ToString()
    {
      return DefaultArgumentFormatter.Format(_expected, false);
    }

    public string DescribeFor(object argument)
    {
      try
      {
        _equivalencyAssertion.Make(_expected, argument);
        return string.Empty;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }

    public bool IsSatisfiedBy(object argument)
    {
      try
      {
        _equivalencyAssertion.Make(_expected, argument);
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}