using System;
using System.Collections.Generic;
using System.Linq;

namespace TddEbook.TddToolkit.NSubstitute
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
      Dictionary<int, Exception> exceptions = new Dictionary<int, Exception>();
      for(var i = 0 ; i < _assertions.Length ; ++i)
      {
        try
        {
          _assertions[i].Make(expected, actual);
        }
        catch (Exception e)
        {
          exceptions.Add(i+1, e);
        }
      }
      if (exceptions.Any())
      {
        throw new MultipleConditionsFailedException(exceptions);
      }
    }
  }
}