using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
//using TddEbook.TddToolkit.Nunit.NUnitExtensions;

namespace TddEbook.TddToolkit.NUnitSpecification
{
  public class AnyAttributeSpecification
  {
    /*
    [Test, Category("DoesNotRunWithCustomRunners"), Ignore("not yet supported by most tools")]
    public void ShouldAllowPassingDifferentObjectsAndPrimitivesThroughParameters
    (
      [Any] int anInt,
      [Any] int anInt2, 
      [Any] string aString, 
      [Any] ISimple interfaceImplementation,
      [Any] IEnumerable<int> anEnumerable, 
      [Any] List<string> concreteList,
      [Any] IEnumerable<ISimple> interfaceImplementationList,
      [Any] ProperValueType value,
      [AnyOtherThan(3,4)] int nonThree
    )
    {
      XAssert.NotEqual(default(int), anInt);
      XAssert.NotEqual(anInt2, anInt);
      Assert.False(string.IsNullOrEmpty(aString));
      Assert.NotNull(interfaceImplementation);
      Assert.Greater(anEnumerable.Count(), 0);
      Assert.Greater(concreteList.Count(), 0);
      Assert.Greater(concreteList.Count(), 0);

      Assert.Greater(interfaceImplementationList.Count(), 0);
      Assert.NotNull(interfaceImplementationList.ToArray()[0]);
      Assert.NotNull(interfaceImplementationList.ToArray()[1]);
      Assert.NotNull(interfaceImplementationList.ToArray()[2]);

      Assert.NotNull(value);

      XAssert.NotEqual(3, nonThree);
    }
    */
  }

  public interface ISimple
  {
    int GetInt();
    string GetString();
    ISimple GetInterface();

    string GetStringProperty { get; }
    Type GetTypeProperty { get; }
    IEnumerable<ISimple> Simples { get; }
  }

  public class ProperValueType : IEquatable<ProperValueType>
  {
    public bool Equals(ProperValueType other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return _a == other._a && Equals(_anArray, other._anArray);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((ProperValueType)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (_a * 397) ^ (_anArray != null ? _anArray.GetHashCode() : 0);
      }
    }

    public static bool operator ==(ProperValueType left, ProperValueType right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(ProperValueType left, ProperValueType right)
    {
      return !Equals(left, right);
    }

    private readonly int _a;
    private readonly IEnumerable<int> _anArray;

    public ProperValueType(int a, IEnumerable<int> anArray)
    {
      _a = a;
      _anArray = anArray;
    }
  }

}
