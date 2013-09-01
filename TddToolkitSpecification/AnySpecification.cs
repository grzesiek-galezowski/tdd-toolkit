using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using TddEbook.TddToolkit;

namespace TddToolkitSpecification
{
  public class AnySpecification
  {
    [Test]
    public void ShouldGenerateDifferentIntegerEachTime()
    {
      //GIVEN
      var int1 = Any.Integer();
      var int2 = Any.Integer();

      //THEN
      Assert.AreNotEqual(int1, int2);
    }

    [Test]
    public void ShouldGenerateDifferentTypeEachTime()
    {
      //GIVEN
      var type1 = Any.InstanceOf<Type>();
      var type2 = Any.InstanceOf<Type>();
      var type3 = Any.InstanceOf<Type>();

      //THEN
      Assert.NotNull(type1);
      Assert.NotNull(type2);
      Assert.NotNull(type3);

      Assert.AreNotEqual(type1, type2);
      Assert.AreNotEqual(type2, type3);
      Assert.AreNotEqual(type3, type1);
    }

    [Test]
    public void ShouldBeAbleToProxyConcreteReturnTypesOfMethods()
    {
      var obj = Any.InstanceOf<ISimple>();

      Assert.AreNotEqual(default(int), obj.GetInt());
      Assert.AreNotEqual(string.Empty, obj.GetString());
      Assert.AreNotEqual(string.Empty, obj.GetStringProperty);
      Assert.NotNull(obj.GetString());
      Assert.NotNull(obj.GetStringProperty);
    }

    [Test]
    public void ShouldBeAbleToProxyMethodsThatReturnInterfaces()
    {
      //GIVEN
      var obj = Any.InstanceOf<ISimple>();
      
      //WHEN
      obj = obj.GetInterface();

      //THEN
      Assert.NotNull(obj);
      Assert.AreNotEqual(default(int), obj.GetInt());
      Assert.AreNotEqual(string.Empty, obj.GetString());
      Assert.AreNotEqual(string.Empty, obj.GetStringProperty);
      Assert.NotNull(obj.GetString());
      Assert.NotNull(obj.GetStringProperty);
      
    }

    [Test]
    public void ShouldAlwaysReturnTheSameValueFromProxiedMethodOnTheSameObject()
    {
      //GIVEN
      var obj = Any.InstanceOf<ISimple>();

      //WHEN
      var valueFirstTime = obj.GetString();
      var valueSecondTime = obj.GetString();

      //THEN
      Assert.AreEqual(valueFirstTime, valueSecondTime);
    }

    [Test]
    public void ShouldAlwaysReturnTheDifferentValueFromProxiedTheSameMethodOnDifferentObject()
    {
      //GIVEN
      var obj = Any.InstanceOf<ISimple>();
      var obj2 = Any.InstanceOf<ISimple>();

      //WHEN
      var valueFromFirstInstance = obj.GetString();
      var valueFromSecondInstance = obj2.GetString();

      //THEN
      Assert.AreNotEqual(valueFromFirstInstance, valueFromSecondInstance);
    }

    [Test]
    public void ShouldGenerateFiniteEnumerables()
    {
      //GIVEN
      var o = Any.InstanceOf<ISimple>();

      //WHEN
      var enumerable = o.Simples;

      //THEN
      foreach (var simple in enumerable)
      {
        Assert.NotNull(simple);
      }
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
  }
}

