using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions
{
  public class ConstraintsViolations
  {
    private readonly List<string> _violations = new List<string>();

    public static ConstraintsViolations Empty()
    {
      return new ConstraintsViolations();
    }

    public void AssertNone()
    {
      Assert.False(_violations.Any(), MessageContainingAll(_violations));
    }

    private string MessageContainingAll(IEnumerable<string> violations)
    {
      return violations.Any() ? violations.Aggregate((a, b) => a + Environment.NewLine + b) : "No violations.";
    }

    public void Add(string violationDetails)
    {
      _violations.Add(violationDetails);
    }
  }
}