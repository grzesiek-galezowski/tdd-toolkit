using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddEbook.TddToolkit
{
  public class ValueTypeTraits
  {
    public ValueTypeTraits()
    {
      IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify = new List<int>();
      RequireAllFieldsReadOnly = true;
      RequireSafeUnequalityToNull = true;
    }

    public static ValueTypeTraits Custom
    {
      get
      {
        return new ValueTypeTraits();
      }
    }

    public List<int> IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify
    {
      get;
      set;
    }

    public bool RequireSafeUnequalityToNull
    {
      get;
      set;
    }

    public bool RequireAllFieldsReadOnly
    {
      get; set;
    }

    public static ValueTypeTraits Default()
    {
      return new ValueTypeTraits();
    }

    public ValueTypeTraits SkipConstructorArgument(int constructorArgumentIndex)
    {
      IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify.Add(constructorArgumentIndex);
      return this;
    }
  }
}
