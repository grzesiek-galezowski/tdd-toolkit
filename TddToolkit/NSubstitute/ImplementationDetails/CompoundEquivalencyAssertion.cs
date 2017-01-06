using System;
using System.Linq;

namespace TddEbook.TddToolkit.NSubstitute.ImplementationDetails
{
  public class CompoundEquivalencyAssertion : EquivalencyAssertion
  {
    private readonly EquivalencyAssertion[] _assertions;

    public CompoundEquivalencyAssertion(EquivalencyAssertion[] assertions)
    {
      _assertions = assertions;
    }

    public void Make(object expected, object actual)
    {
      MultipleConditionsExecutionLoop.Execute(
        _assertions.Select(a => new Action<object>(o => a.Make(expected, o)))
        .ToList(), actual);
    }
  }
}