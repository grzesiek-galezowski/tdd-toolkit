using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TddEbook.TddToolkit.NUnitExtensions
{
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  public class AnyAttribute  : ParameterDataAttribute
  {
     public override IEnumerable GetData(ParameterInfo parameter)
     {
       return new[] { Any.Instance(parameter.ParameterType)};
     }
  }

  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  public class AnyOtherThanAttribute : ParameterDataAttribute
  {
    private object[] _omittedValues;

    public AnyOtherThanAttribute(params object[] omittedValues)
    {
      _omittedValues = omittedValues;
    }

    public override IEnumerable GetData(ParameterInfo parameter)
    {
      return new[] { Any.InstanceOtherThanObjects(parameter.ParameterType, new [] {_omittedValues}) };
    }
  }


}
