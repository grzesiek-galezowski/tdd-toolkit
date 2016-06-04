using NUnit.Framework;
using System;
using System.Collections;
using System.Reflection;

namespace TddEbook.TddToolkit.Nunit.NUnitExtensions
{
  /*
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
    private readonly object[] _omittedValues;

    public AnyOtherThanAttribute(params object[] omittedValues)
    {
      _omittedValues = omittedValues;
    }

    public override IEnumerable GetData(ParameterInfo parameter)
    {
      return new[] { Any.InstanceOtherThanObjects(parameter.ParameterType, new object[] { _omittedValues }) };
    }
  }

*/
}
