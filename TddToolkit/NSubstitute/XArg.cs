using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Core.Arguments;
using TddEbook.TddToolkit.NSubstitute.ImplementationDetails;

namespace TddEbook.TddToolkit.NSubstitute
{

  public static class XArg
  {
    public static T IsLike<T>(T expected)
    {
      return IsEquivalentTo(expected, new ObjectComparerEquivalencyAssertion());
    }

    public static T IsNotLike<T>(T expected)
    {
      return IsEquivalentTo(expected, new ObjectComparerNonEquivalencyAssertion());
    }

    public static T[] IsNot<T>(IEnumerable<T> unexpected)
    {
      return Arg.Is<T[]>(arg => !arg.SequenceEqual(unexpected));
    }

    public static T IsEquivalentTo<T>(T obj, params EquivalencyAssertion[] equivalencyAssertions)
    {
      equivalencyAssertions
        .Should().NotBeEmpty("at least one condition should be specified");

      var equivalentArgumentMatcher = new EquivalentArgumentMatcher<T>(obj,
        DetermineEquivalencyAssertion(equivalencyAssertions));
      EnqueueMatcher<T>(equivalentArgumentMatcher);
      return default(T);
    }

    public static T Passing<T>(params Action<T>[] assertions)
    {
      assertions
        .Should().NotBeEmpty("at least one condition should be specified");

      var lambdaMatcher = new LambdaArgumentMatcher<T>(assertions);
      EnqueueMatcher<T>(lambdaMatcher);
      return default(T);
    }



    private static void EnqueueMatcher<T>(IArgumentMatcher lambdaMatcher)
    {
      SubstitutionContext.Current.EnqueueArgumentSpecification(
        new ArgumentSpecification(typeof(T), 
          lambdaMatcher));
    }
    public static T IsEquivalentTo<T>(T obj)
    {
      return IsEquivalentTo(obj, FluentAssertionsEquivalencyAssertion<T>.Default());
    }

    private static EquivalencyAssertion DetermineEquivalencyAssertion(EquivalencyAssertion[] equivalencyAssertions)
    {
      return equivalencyAssertions.Length == 1 ? 
        equivalencyAssertions.First()
        : new CompoundEquivalencyAssertion(equivalencyAssertions);
    }
  }
}