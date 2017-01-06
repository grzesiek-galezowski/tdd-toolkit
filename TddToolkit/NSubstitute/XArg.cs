using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Core.Arguments;

namespace TddEbook.TddToolkit.NSubstitute
{

  public static class XArg
  {
    public static T IsLike<T>(T expected)
    {
      return IsEquivalentTo<T>(expected, new ObjectComparerEquivalencyAssertion());
    }

    public static T IsNotLike<T>(T expected)
    {
      return IsEquivalentTo<T>(expected, new ObjectComparerNonEquivalencyAssertion());
    }

    public static T[] IsNot<T>(IEnumerable<T> unexpected)
    {
      return Arg.Is<T[]>(arg => !arg.SequenceEqual(unexpected));
    }

    public static T IsEquivalentTo<T>(T obj, params EquivalencyAssertion[] equivalencyAssertions)
    {
      if(!equivalencyAssertions.Any()) throw new Exception("no conditions specified");
      var equivalentArgumentMatcher = new EquivalentArgumentMatcher<T>(obj,
        DetermineEquivalencyAssertion<T>(equivalencyAssertions));
      SubstitutionContext.Current.EnqueueArgumentSpecification(new ArgumentSpecification(typeof(T),
        equivalentArgumentMatcher));
      return default(T);
    }

    private static EquivalencyAssertion DetermineEquivalencyAssertion<T>(EquivalencyAssertion[] equivalencyAssertions)
    {
      return equivalencyAssertions.Length == 1 ? 
        equivalencyAssertions.First()
        : new CompoundEquivalencyAssertion(equivalencyAssertions);
    }

    public static T IsEquivalentTo<T>(T obj)
    {
      return IsEquivalentTo<T>(obj, FluentAssertionsEquivalencyAssertion<T>.Default());
    }
  }
}