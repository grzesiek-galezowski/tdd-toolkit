using System;
using FluentAssertions;
using FluentAssertions.Equivalency;

namespace TddEbook.TddToolkit.NSubstitute
{
  public class FluentAssertionsEquivalencyAssertion<T> : EquivalencyAssertion
  {
    private readonly Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> _options;

    public FluentAssertionsEquivalencyAssertion(Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> options)
    {
      _options = options;
    }

    public void Make(object expected, object actual)
    {
      ((T) actual).ShouldBeEquivalentTo(expected, _options);
    }

    public static EquivalencyAssertion Default()
    {
      return new FluentAssertionsEquivalencyAssertion<T>(x => x.IncludingAllDeclaredProperties());
    }

    public static FluentAssertionsEquivalencyAssertion<T> With<T>(Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> options)
    {
      return new FluentAssertionsEquivalencyAssertion<T>(options);
    }
  }
}