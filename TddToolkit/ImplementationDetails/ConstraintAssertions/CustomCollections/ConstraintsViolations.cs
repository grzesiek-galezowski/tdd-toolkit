using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions
{
  public interface IConstraintsViolations
  {
    void AssertNone();
    void Add(string violationDetails);
  }

  public class ConstraintsViolations : IConstraintsViolations
  {
    private readonly List<string> _violations = new List<string>();

    public static ConstraintsViolations Empty()
    {
      return new ConstraintsViolations();
    }

    public void AssertNone()
    {
      _violations.Any().Should().BeFalse("The type should not fail any value type assertions, but failed: " 
        + Environment.NewLine + MessageContainingAll(_violations));
    }

    private static string MessageContainingAll(IEnumerable<string> violations)
    {
      return violations.Any() ? violations.Aggregate((a, b) => a + Environment.NewLine + b) : "No violations.";
    }

    public void Add(string violationDetails)
    {
      _violations.Add(violationDetails);
    }
  }
}