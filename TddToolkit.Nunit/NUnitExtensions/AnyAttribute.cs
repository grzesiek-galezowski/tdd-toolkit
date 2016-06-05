using NUnit.Framework;
using System;
using System.Collections;
using NSubstitute;
using NUnit.Framework.Interfaces;

namespace TddEbook.TddToolkit.Nunit.NUnitExtensions
{

  [AttributeUsage(AttributeTargets.Parameter)]
  public class AnyAttribute : DataAttribute, IParameterDataSource
  {
    public IEnumerable GetData(IParameterInfo parameter)
    {
      return new[] { Any.Instance(parameter.ParameterType) };
    }
  }

  [AttributeUsage(AttributeTargets.Parameter)]
  public class SubstituteAttribute : DataAttribute, IParameterDataSource
  {
    public IEnumerable GetData(IParameterInfo parameter)
    {
      return new[] { Substitute.For(new[] { parameter.ParameterType}, new object[] {}) };
    }
  }

  [AttributeUsage(AttributeTargets.Parameter)]
  public class AnyOtherThanAttribute : DataAttribute, IParameterDataSource
  {
    private readonly object[] _omittedValues;

    public AnyOtherThanAttribute(params object[] omittedValues)
    {
      _omittedValues = omittedValues;
    }

    public IEnumerable GetData(IParameterInfo parameter)
    {
      return new[] { Any.InstanceOtherThanObjects(parameter.ParameterType, new object[] { _omittedValues }) };
    }
  }
}
