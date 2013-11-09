using FluentAssertions;
using FluentAssertions.Primitives;
using FluentAssertions.Types;

namespace TddEbook.TddToolkit.Helpers.FluentAssertions
{
  public static class FluentAssertionExtensions
  {
    public static AndConstraint<ObjectAssertions> BeLike(this ObjectAssertions o, object expected)
    {
      XAssert.Alike(expected, o.Subject);
      return new AndConstraint<ObjectAssertions>(o);
    }

    public static AndConstraint<ObjectAssertions> NotBeLike(this ObjectAssertions o, object expected)
    {
      XAssert.NotAlike(expected, o.Subject);
      return new AndConstraint<ObjectAssertions>(o);
    }

    public static AndConstraint<TypeAssertions> BehaveLikeValueType(this TypeAssertions o)
    {
      XAssert.IsValue(o.Subject);
      return new AndConstraint<TypeAssertions>(o);
    }
  }
}
