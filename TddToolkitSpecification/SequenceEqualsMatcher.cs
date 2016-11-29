using System;
using System.Collections.Generic;
using System.Linq;

namespace TddEbook.TddToolkitSpecification
{
  public class SequenceEqualsMatcher
  {
    private readonly List<int> _expected;
    private readonly List<string> failures = new List<string>();

    public SequenceEqualsMatcher(List<int> expected)
    {
      _expected = expected;
    }

    public bool Matches(IEnumerable<int> actual)
    {
      var sequenceEqual = actual.SequenceEqual(_expected);
      if (!sequenceEqual)
      {
        failures.Add(EnumerableToString(actual) + " does not match " + EnumerableToString(_expected));
      }
      return sequenceEqual;
    }

    private string EnumerableToString(IEnumerable<int> enumerable)
    {
      if (!enumerable.Any())
      {
        return "EMPTY";
      }
      return "{" + string.Join(", ", enumerable) + "}";
    }

    public override string ToString()
    {
      return string.Join(Environment.NewLine, failures);
    }
  }
}