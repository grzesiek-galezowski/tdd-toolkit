using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TddEbook.TddToolkit;
using NSubstitute;

namespace TddEbook.TddToolkitSpecification
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
      var type1 = Any.Instance<Type>();
      var type2 = Any.Instance<Type>();
      var type3 = Any.Instance<Type>();

      //THEN
      XAssert.All(assert =>
        {
          assert.NotNull(type1);
          assert.NotNull(type2);
          assert.NotNull(type3);
        });

      XAssert.All(assert =>
        {
          assert.NotEqual(type1, type2);
          assert.NotEqual(type2, type3);
          assert.NotEqual(type3, type1);
        });
    }

    [Test]
    public void ShouldBeAbleToProxyConcreteReturnTypesOfMethods()
    {
      var obj = Any.Instance<ISimple>();

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
      var obj = Any.Instance<ISimple>();
      
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
      var obj = Any.Instance<ISimple>();

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
      var obj = Any.Instance<ISimple>();
      var obj2 = Any.Instance<ISimple>();

      //WHEN
      var valueFromFirstInstance = obj.GetString();
      var valueFromSecondInstance = obj2.GetString();

      //THEN
      XAssert.NotEqual(valueFromFirstInstance, valueFromSecondInstance);
    }

    [Test]
    public void ShouldCreateNonNullUri()
    {
      Assert.NotNull(Any.Uri());
    }

    [Test]
    public void ShouldGenerateFiniteEnumerables()
    {
      //GIVEN
      var o = Any.Instance<ISimple>();

      //WHEN
      var enumerable = o.Simples;

      //THEN

      foreach (var simple in enumerable) { XAssert.NotNull(simple); }
    }

    [Test]
    public void ShouldGenerateMembersReturningTypeOfType()
    {
      //GIVEN
      var obj1 = Any.Instance<ISimple>();
      var obj2 = Any.Instance<ISimple>();

      //THEN
      XAssert.All(assert =>
        {
          assert.NotNull(obj1.GetTypeProperty);
          assert.NotNull(obj2.GetTypeProperty);
          assert.NotEqual(obj1.GetTypeProperty, obj2.GetTypeProperty);
          assert.Equal(obj1.GetTypeProperty, obj1.GetTypeProperty);
          assert.Equal(obj2.GetTypeProperty, obj2.GetTypeProperty);
        });
    }

    [Test]
    public void ShouldBeAbleToGenerateInstancesOfConcreteClassesWithInterfacesAsTheirConstructorArguments()
    {
      //GIVEN
      var createdProxy = Any.Instance<ObjectWithInterfaceInConstructor>();

      //THEN
      XAssert.NotNull(createdProxy._constructorArgument);
      XAssert.NotNull(createdProxy._constructorNestedArgument);
    }

    [Test]
    public void ShouldBeAbleToGenerateInstancesOfAbstractClasses()
    {
      //GIVEN
      var createdProxy = Any.Instance<AbstractObjectWithInterfaceInConstructor>();

      //THEN
      XAssert.All(assert =>
        {
          assert.NotNull(createdProxy._constructorArgument);
          assert.NotNull(createdProxy._constructorNestedArgument);
          assert.NotEqual(default(int), createdProxy.AbstractInt);
          assert.NotEqual(default(int), createdProxy.SettableInt);
        });
    }

    [Test]
    public void ShouldOverrideVirtualMethodsThatReturnDefaultTypeValuesOnAbstractClassProxy()
    {
      //GIVEN
      var obj = Any.Instance<AbstractObjectWithVirtualMethods>();

      //THEN
      XAssert.NotEqual(default(string), obj.GetSomething());
      XAssert.NotEqual("Something", obj.GetSomething2());
    }

    [Test]
    public void ShouldOverrideVirtualMethodsThatThrowExceptionsOnAbstractClassProxy()
    {
      //GIVEN
      var obj = Any.Instance<AbstractObjectWithVirtualMethods>();

      //THEN
      XAssert.NotEqual(default(string), obj.GetSomethingButThrowExceptionWhileGettingIt());
    }

    [Test]
    public void ShouldNotCreateTheSameMethodInfoTwiceInARow()
    {
      //GIVEN
      var x = Any.Instance<MethodInfo>();
      var y = Any.Instance<MethodInfo>();
      
      //THEN
      XAssert.NotAlike(x,y);

    }

    [Test]
    public void ShouldCreateDifferentExceptionEachTime()
    {
      //GIVEN
      var exception1 = Any.Instance<Exception>();
      var exception2 = Any.Instance<Exception>();
      
      //THEN
      XAssert.NotAlike(exception2, exception1);
    }

    [Test]
    public void ShouldGenerateDifferentValueEachTimeAndNotAmongPassedOnesWhenAskedToCreateAnyValueBesidesGiven()
    {
      //WHEN
      var int1 = Any.ValueOtherThan(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
      var int2 = Any.ValueOtherThan(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

      //THEN
      XAssert.NotEqual(int1, int2);
      Assert.That(int1, Is.Not.InRange(1,10));
      Assert.That(int2, Is.Not.InRange(1, 10));
    }

    [Test]
    [Description("Non-deterministic statement - check it out")]
    public void ShouldGeneratePickNextValueEachTimeFromPassedOnesWhenAskedToCreateAnyValueFromGiven()
    {
      //WHEN
      var int1 = Any.From(Enumerable.Range(1, 3).ToArray());
      var int2 = Any.From(Enumerable.Range(1, 3).ToArray()); //should pick next element
      var int3 = Any.From(Enumerable.Range(1, 3).ToArray()); //should pick next element
      var int4 = Any.From(Enumerable.Range(1, 3).ToArray()); //should pick next element
      var int5 = Any.From(Enumerable.Range(1, 2).ToArray()); //should start from beginning
      var int6 = Any.From(Enumerable.Range(1, 4).ToArray()); //should start from beginning

      //THEN
      XAssert.All(assert =>
      {
        assert.Equal(1, int1);
        assert.Equal(2, int2);
        assert.Equal(3, int3);
        assert.Equal(1, int4);
        assert.Equal(1, int5);
        assert.Equal(1, int6);
      });
    }

    [Test]
    public void ShouldGenerateStringAccordingtoRegex()
    {
      //GIVEN
      const string exampleRegex = @"content/([A-Za-z0-9\-]+)\.aspx$";
      
      //WHEN
      var result = Any.StringMatching(exampleRegex);
      
      //THEN
      Assert.True(Regex.IsMatch(result, exampleRegex));
    }

    [Test]
    public void ShouldGenerateStringOfGivenLength()
    {
      //GIVEN
      var stringLength = Any.Integer();

      //WHEN
      var str = Any.StringOfLength(stringLength);

      //THEN
      XAssert.Equal(stringLength, str.Length);
    }

    [Test]
    public void ShouldCreateSortedSetWithThreeDistinctValues()
    {
      //WHEN
      var set = Any.SortedSet<int>();

      //THEN
      CollectionAssert.IsOrdered(set);
      CollectionAssert.AllItemsAreUnique(set);
      XAssert.Equal(3, set.Count);
    }

    [Test]
    public void ShouldBeAbleToGenerateDistinctLettersEachTime()
    {
      //WHEN
      var char1 = Any.AlphaChar();
      var char2 = Any.AlphaChar();
      var char3 = Any.AlphaChar();

      //THEN
      XAssert.All(assert =>
        {
          assert.NotEqual(char1, char2);
          assert.NotEqual(char2, char3);
          assert.True(Char.IsLetter(char1));
          assert.True(Char.IsLetter(char2));
          assert.True(Char.IsLetter(char3));
        });
    }

    [Test]
    public void ShouldBeAbleToGenerateDistinctDigitsEachTime()
    {
      //WHEN
      var char1 = Any.DigitChar();
      var char2 = Any.DigitChar();
      var char3 = Any.DigitChar();

      //THEN
      XAssert.All(assert =>
        {
          assert.NotEqual(char1, char2);
          assert.NotEqual(char2, char3);
          assert.True(Char.IsDigit(char1));
          assert.True(Char.IsDigit(char2));
          assert.True(Char.IsDigit(char3));
        });
    }

    [Test, Timeout(1000)]
    public void ShouldHandleEmptyExcludedStringsWhenGeneratingAnyStringNotContainingGiven()
    {
      Assert.DoesNotThrow(() => Any.StringNotContaining(string.Empty));
    }

    [Test]
    public void ShouldBeAbleToGenerateBothPrimitiveTypeInstanceAndInterfaceUsingNewInstanceMethod()
    {
      var primitive = Any.Instance<int>();
      var interfaceImplementation = Any.Instance<ISimple>();

      XAssert.All(assert =>
        {
          assert.NotNull(interfaceImplementation);
          assert.NotEqual(default(int), primitive);
        });
    }


    [Test]
    public void ShouldSupportRecursiveInterfacesWithLists()
    {
      var factories = Any.Enumerable<RecursiveInterface>().ToList();

      var x = factories[0];
      var y = x.GetNested();
      var y2 = y[0];
      var z = y2.Nested;
      var x1 = z.GetNested()[1];
      var x2 = x1.Nested.Number;

      Assert.AreNotEqual(default(int), x2);
    }

    [Test]
    public void ShouldSupportGeneratingOtherObjectsThanNull()
    {
      Assert.DoesNotThrow(() => Any.OtherThan<string>(null));
    }

    [Test]
    public void ShouldSupportRecursiveInterfacesWithDictionaries()
    {
      var factories = Any.Enumerable<RecursiveInterface>().ToList();

      var x = factories[0];
      var y = x.NestedAsDictionary;
      var y1 = y.Keys.First();
      var y2 = y.Values.First();
      
      XAssert.All(assert =>
      {
        assert.Equal(3, y.Count);
        assert.NotNull(y1);
        assert.NotNull(y2);
      });
    }

    [Test]
    public void ShouldSupportGeneratingRangedCollections()
    {
      const int anyCount = 4;
      var list = Any.List<RecursiveInterface>(anyCount);
      var array = Any.Array<RecursiveInterface>(anyCount);
      var set = Any.Set<RecursiveInterface>(anyCount);
      var dictionary = Any.Dictionary<RecursiveInterface, ISimple>(anyCount);
      var sortedList = Any.SortedList<string, ISimple>(anyCount);
      var sortedDictionary = Any.SortedDictionary<string, ISimple>(anyCount);
      var sortedEnumerable = Any.EnumerableSortedDescending<string>(anyCount);
      var enumerable = Any.Enumerable<RecursiveInterface>(anyCount);

      XAssert.All(assert =>
      {
        assert.Equal(anyCount, list.Count);
        assert.Equal(anyCount, enumerable.Count());
        assert.Equal(anyCount, array.Length);
        assert.Equal(anyCount, set.Count);
        assert.Equal(anyCount, dictionary.Count);
        assert.Equal(anyCount, sortedList.Count);
        assert.Equal(anyCount, sortedDictionary.Count);
        assert.Equal(anyCount, sortedEnumerable.Count());
      });
    }

    [Test]
    public void ShouldAllowCreatingCustomCollectionInstances()
    {
      var customCollection = Any.Instance<MyOwnCollection<RecursiveInterface>>();

      XAssert.Equal(3, customCollection.Count);
      foreach (var recursiveInterface in customCollection)
      {
        XAssert.NotNull(recursiveInterface);
      }
    }

    [Test]
    public void ShouldSupportCreationOfKeyValuePairs()
    {
      var kvp = Any.Instance<KeyValuePair<string, RecursiveInterface>>();
      XAssert.All(assert =>
        {
          assert.NotNull(kvp.Key);
          assert.NotNull(kvp.Value);
        });
    }

    [Test]
    public void ShouldSupportActions()
    {
      //WHEN
      var action = Any.Instance<Action<ISimple, string>>();

      //THEN
      Assert.NotNull(action);
    }

    [Test]
    public void ShouldAllowCreatingDifferentMaybesOfConcreteClasses()
    {
      var maybeString1 = Any.Instance<Maybe<string>>();
      var maybeString2 = Any.Instance<Maybe<string>>();

      XAssert.NotEqual(maybeString1, maybeString2);
    }

    [Test]
    public void ShouldAllowCreatingDifferentMaybesOfInterfaces()
    {
      var maybeImplementation1 = Any.Instance<Maybe<RecursiveInterface>>();
      var maybeImplementation2 = Any.Instance<Maybe<RecursiveInterface>>();

      XAssert.NotEqual(maybeImplementation1, maybeImplementation2);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndOverrideDefaultValues()
    {
      //GIVEN
      var instance = Any.WrapperOver(Substitute.For<RecursiveInterface>());

      //WHEN
      var result = instance.Object.Number;

      //THEN
      XAssert.NotEqual(default(int), result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndNotOverrideStubbedValues()
    {
      //GIVEN
      var instance = Any.WrapperOver(Substitute.For<RecursiveInterface>(), s => s.Number.Returns(44543));
      
      //WHEN
      var result = instance.Object.Number;

      //THEN
      XAssert.Equal(44543, result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndStillAllowVerifyingCalls()
    {
      //GIVEN
      var instance = Any.WrapperOver(Substitute.For<RecursiveInterface>());

      //WHEN
      instance.Object.VoidMethod();

      //THEN
      instance.Prototype.Received(1).VoidMethod();
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndOverrideNullInterfaceReturnValues()
    {
      //GIVEN
      var instance = Any.WrapperOver(Substitute.For<RecursiveInterface>());
      instance.Prototype.Nested.Returns(null as RecursiveInterface);

      //WHEN
      var result = instance.Object.Nested;

      //THEN
      Assert.NotNull(result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndSkipOverridingResultsStubbedWithNonDefaultValues()
    {
      var instance = Any.WrapperOver(Substitute.For<RecursiveInterface>());
      var anotherInstance = Substitute.For<RecursiveInterface>();
      instance.Prototype.Nested.Returns(anotherInstance);

      XAssert.Equal(anotherInstance, instance.Object.Nested);
      XAssert.Equal(instance.Prototype.Nested, instance.Object.Nested);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndAllowSkippingOverrideOfMethodDefaultReturnValue()
    {
      var instance = Any.WrapperOver(Substitute.For<RecursiveInterface>(), s => s.Nested.Returns(null as RecursiveInterface))
        .NoOverrideOf(m => m.Nested)
        .NoOverrideOf(m => m.GetNested());

      Assert.Null(instance.Object.GetNested());
      Assert.Null(instance.Object.Nested);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndAllowForcingOverrideOfMethodReturnValueDespiteItBeingNotDefault()
    {
      var instance = Any.WrapperOver(Substitute.For<RecursiveInterface>(), s =>
        {
          s.Number.Returns(-9999);
          s.GetNumber().Returns(-9998);
        }).ForceOverrideOf(m => m.Number).ForceOverrideOf(m => m.GetNumber());

      XAssert.NotEqual(default(int), instance.Object.Number);
      XAssert.NotEqual(default(int), instance.Object.GetNumber());
      XAssert.NotEqual(-9999, instance.Object.Number);
      XAssert.NotEqual(-9998, instance.Object.GetNumber());
    }

    [Test]
    public void ShouldBeAbleToGenerateFiniteInstancesOfGenericEnumerators()
    {
      //GIVEN
      var enumerator = Any.Instance<IEnumerator<string>>();

      //WHEN
      var element1 = enumerator.Current;
      enumerator.MoveNext();
      var element2 = enumerator.Current;

      //THEN
      XAssert.NotEqual(element2, element1);
    }

    [Test]
    public void ShouldBeAbleToGenerateFiniteInstancesOfNonGenericEnumerators()
    {
      //GIVEN
      var enumerator = Any.Instance<IEnumerator>();

      //WHEN
      enumerator.MoveNext();
      var element1 = enumerator.Current;
      enumerator.MoveNext();
      var element2 = enumerator.Current;

      //THEN
      XAssert.NotEqual(element2, element1);
    }


    [Test]
    public void ShouldBeAbleToGenerateInstancesOfGenericKeyValueEnumerables()
    {
      //GIVEN
      var instance = Any.Instance<IObservableConcurrentDictionary<string, string>>();

      //WHEN
      var element1 = instance.GetEnumerator().Current;
      instance.GetEnumerator().MoveNext();
      var element2 = instance.GetEnumerator().Current;

      //THEN
      XAssert.NotEqual(element2, element1);
    }

    [Test]
    public void ShouldAllowGeneratingIntegersFromSequence()
    {
      var value1 = Any.IntegerFromSequence(startingValue: 12, step: 112);
      var value2 = Any.IntegerFromSequence(startingValue: 12, step: 112);

      XAssert.Equal(value1, value2 - 112);
      Assert.Greater(value1, 12);
    }

    public interface RecursiveInterface
    {
      List<RecursiveInterface> GetNestedWithArguments(int a, int b);
      List<RecursiveInterface> GetNested();
      void VoidMethod();
      IDictionary<string, RecursiveInterface> NestedAsDictionary { get; }
      RecursiveInterface Nested { get; }
      int Number { get; }
      int GetNumber();
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
      public readonly ISimple _constructorArgument;
      private readonly string _b;
      public readonly ObjectWithInterfaceInConstructor _constructorNestedArgument;

      public ObjectWithInterfaceInConstructor(
        int a, 
        ISimple constructorArgument, 
        string b, 
        ObjectWithInterfaceInConstructor constructorNestedArgument)
      {
        _a = a;
        _constructorArgument = constructorArgument;
        _b = b;
        _constructorNestedArgument = constructorNestedArgument;
      }
    }

    public abstract class AbstractObjectWithInterfaceInConstructor
    {
      private readonly int _a;
      public readonly ISimple _constructorArgument;
      private readonly string _b;
      public readonly ObjectWithInterfaceInConstructor _constructorNestedArgument;

      public AbstractObjectWithInterfaceInConstructor(
        int a,
        ISimple constructorArgument,
        string b,
        ObjectWithInterfaceInConstructor constructorNestedArgument)
      {
        _a = a;
        _constructorArgument = constructorArgument;
        _b = b;
        _constructorNestedArgument = constructorNestedArgument;
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

  public interface IObservableConcurrentDictionary<TKey, TValue> 
    : IObservable<Tuple<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>
  {
    void TryAdd(TKey key, TValue value);
    TValue this[TKey key] { get; set; }
    void TryRemove(TKey key);
    bool TryGetValue(TKey key, out TValue value);
  }


  public class MyOwnCollection<T> : ICollection<T>
  {
    List<T> _list = new List<T>();

    public IEnumerator<T> GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    public void Add(T item)
    {
      _list.Add(item);
    }

    public void Clear()
    {
      _list.Clear();
    }

    public bool Contains(T item)
    {
      return _list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      _list.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
      return _list.Remove(item);
    }

    public int Count { get { return _list.Count; } }
    public bool IsReadOnly { get { return false; } }
  }
}


