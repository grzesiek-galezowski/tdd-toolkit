using System;
using System.Diagnostics.CodeAnalysis;

namespace TddEbook.TddToolkitSpecification.Fixtures
{
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [Serializable]
  public class ComplexObjectWithFactoryMethodAndRecursiveConstructor
  {
    private readonly string _initialValue;

    private ComplexObjectWithFactoryMethodAndRecursiveConstructor(string parameterName)
    {
      if (string.IsNullOrEmpty(parameterName))
      {
        if (parameterName != null)
        {
          IsEmpty = true;
        }
        else
        {
          throw new ArgumentNullException("parameterName");
        }
      }

      _initialValue = parameterName;
    }

    public ComplexObjectWithFactoryMethodAndRecursiveConstructor(
      ComplexObjectWithFactoryMethodAndRecursiveConstructor obj) : this(obj._initialValue)
    {
    }

    public static ComplexObjectWithFactoryMethodAndRecursiveConstructor Create(string parameterName)
    {
      ComplexObjectWithFactoryMethodAndRecursiveConstructor createdWrapper =
        new ComplexObjectWithFactoryMethodAndRecursiveConstructor(parameterName);
      return createdWrapper;
    }

    public static int GetInt()
    {
      return 123;
    }

    public static ComplexObjectWithFactoryMethodAndRecursiveConstructor Empty
      => new ComplexObjectWithFactoryMethodAndRecursiveConstructor(string.Empty);

    private bool IsEmpty { get; set; }

    public override string ToString()
    {
      return _initialValue;
    }

    public override int GetHashCode()
    {
      return _initialValue.GetHashCode();
    }

  }
}