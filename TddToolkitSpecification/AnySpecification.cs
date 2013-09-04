using System;
using System.Collections.Generic;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.Helpers.NUnit;

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
      XAssert.NotEqual(int1, int2);
    }

    [Test]
    public void ShouldGenerateDifferentTypeEachTime()
    {
      //GIVEN
      var type1 = Any.InstanceOf<Type>();
      var type2 = Any.InstanceOf<Type>();
      var type3 = Any.InstanceOf<Type>();

      //THEN
      XAssert.NotNull(type1);
      XAssert.NotNull(type2);
      XAssert.NotNull(type3);

      XAssert.NotEqual(type1, type2);
      XAssert.NotEqual(type2, type3);
      XAssert.NotEqual(type3, type1);
    }

    [Test]
    public void ShouldBeAbleToProxyConcreteReturnTypesOfMethods()
    {
      var obj = Any.InstanceOf<ISimple>();

      XAssert.NotEqual(default(int), obj.GetInt());
      XAssert.NotEqual(string.Empty, obj.GetString());
      XAssert.NotEqual(string.Empty, obj.GetStringProperty);
      XAssert.NotNull(obj.GetString());
      XAssert.NotNull(obj.GetStringProperty);
    }

    [Test]
    public void ShouldBeAbleToProxyMethodsThatReturnInterfaces()
    {
      //GIVEN
      var obj = Any.InstanceOf<ISimple>();
      
      //WHEN
      obj = obj.GetInterface();

      //THEN
      XAssert.NotNull(obj);
      XAssert.NotEqual(default(int), obj.GetInt());
      XAssert.NotEqual(string.Empty, obj.GetString());
      XAssert.NotEqual(string.Empty, obj.GetStringProperty);
      XAssert.NotNull(obj.GetString());
      XAssert.NotNull(obj.GetStringProperty);
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
      XAssert.Equal(valueFirstTime, valueSecondTime);
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
      XAssert.NotEqual(valueFromFirstInstance, valueFromSecondInstance);
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
        XAssert.NotNull(simple);
      }
    }

    [Test]
    public void ShouldGenerateMembersReturningTypeOfType()
    {
      //GIVEN
      var obj1 = Any.InstanceOf<ISimple>();
      var obj2 = Any.InstanceOf<ISimple>();

      //THEN
      XAssert.NotNull(obj1.GetTypeProperty);
      XAssert.NotNull(obj2.GetTypeProperty);
      XAssert.NotEqual(obj1.GetTypeProperty, obj2.GetTypeProperty);
      XAssert.Equal(obj1.GetTypeProperty, obj1.GetTypeProperty);
      XAssert.Equal(obj2.GetTypeProperty, obj2.GetTypeProperty);
    }

    [Test]
    public void ShouldBeAbleToGenerateInstancesOfConcreteClassesWithInterfacesAsTheirConstructorArguments()
    {
      //GIVEN
      var createdProxy = Any.InstanceOf<ObjectWithInterfaceInConstructor>();

      //THEN
      XAssert.NotNull(createdProxy.ConstructorArgument);
      XAssert.NotNull(createdProxy.ConstructorNestedArgument);
    }

    [Test]
    public void ShouldBeAbleToGenerateInstancesOfAbstractClasses()
    {
      //GIVEN
      var createdProxy = Any.InstanceOf<AbstractObjectWithInterfaceInConstructor>();

      //THEN
      XAssert.NotNull(createdProxy.ConstructorArgument);
      XAssert.NotNull(createdProxy.ConstructorNestedArgument);
      XAssert.NotEqual(default(int), createdProxy.AbstractInt);
      XAssert.NotEqual(default(int), createdProxy.SettableInt);
    }

    [Test]
    public void ShouldOverrideVirtualMethodsThatReturnDefaultTypeValuesOnAbstractClassProxy()
    {
      //GIVEN
      var obj = Any.InstanceOf<AbstractObjectWithVirtualMethods>();

      //THEN
      XAssert.NotEqual(default(string), obj.GetSomething());
      XAssert.NotEqual("Something", obj.GetSomething2());
    }

    [Test]
    public void ShouldOverrideVirtualMethodsThatThrowExceptionsOnAbstractClassProxy()
    {
      //GIVEN
      var obj = Any.InstanceOf<AbstractObjectWithVirtualMethods>();

      //THEN
      XAssert.NotEqual(default(string), obj.GetSomethingButThrowExceptionWhileGettingIt());
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
    
    public class ObjectWithInterfaceInConstructor
    {
      private readonly int _a;
      public readonly ISimple ConstructorArgument;
      private readonly string _b;
      public readonly ObjectWithInterfaceInConstructor ConstructorNestedArgument;

      public ObjectWithInterfaceInConstructor(
        int a, 
        ISimple constructorArgument, 
        string b, 
        ObjectWithInterfaceInConstructor constructorNestedArgument)
      {
        _a = a;
        ConstructorArgument = constructorArgument;
        _b = b;
        ConstructorNestedArgument = constructorNestedArgument;
      }
    }

    public abstract class AbstractObjectWithInterfaceInConstructor
    {
      private readonly int _a;
      public readonly ISimple ConstructorArgument;
      private readonly string _b;
      public readonly ObjectWithInterfaceInConstructor ConstructorNestedArgument;

      public AbstractObjectWithInterfaceInConstructor(
        int a,
        ISimple constructorArgument,
        string b,
        ObjectWithInterfaceInConstructor constructorNestedArgument)
      {
        _a = a;
        ConstructorArgument = constructorArgument;
        _b = b;
        ConstructorNestedArgument = constructorNestedArgument;
      }

      public abstract int AbstractInt { get; }

      public int SettableInt { get; set; }
    }

    public abstract class AbstractObjectWithVirtualMethods
    {
      public virtual string GetSomething()
      {
        return default(string);
      }

      public virtual string GetSomething2()
      {
        return "something";
      }

      public virtual string GetSomethingButThrowExceptionWhileGettingIt()
      {
        throw new Exception("Let's suppose dummy data cause this method to throw exception");
      }
    }

  }

}

