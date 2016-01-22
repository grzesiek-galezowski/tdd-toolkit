using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using FluentAssertions;

namespace TddEbook.TddToolkit.ImplementationDetails.Common
{
  //TODO examine similarities with RecordedAssertions
  public class AssertionRecorder
  {
    private int _assertionNumber = 1;
    private readonly List<AssertionFailed> _exceptions = new List<AssertionFailed>();

    internal void AssertTrue()
    {
      XAssert.Equal(0, _exceptions.Count, ExtractMessagesFromAll(_exceptions));
    }

    private string ExtractMessagesFromAll(List<AssertionFailed> failedAssertions)
    {
      string result = "the following assertions shouldn't have failed: " + Environment.NewLine;

      foreach (var failedAssertion in failedAssertions)
      {
        result += failedAssertion.Header();
      }

      foreach (var failedAssertion in failedAssertions)
      {
        result += string.Format("{0}{1}", failedAssertion, Environment.NewLine);
      }

      return result;
    }

    public void Equal<T>(T expected, T actual)
    {
      LogException(() => XAssert.Equal(expected, actual));
    }

    public void CollectionsEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
    {
      LogException(() => XAssert.CollectionsEqual(expected, actual));
    }

    public void Contains(string expected, string actual)
    {
      LogException(() => expected.Should().Contain(actual));
    }

    public void Equal<T>(T expected, T actual, string message)
    {
      LogException(() => XAssert.Equal(expected, actual, message));
    }

    public void Equivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message)
    {
      LogException(() => actual.Should().BeEquivalentTo(expected, "{0}", message));
    }

    public void Equivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual)
    {
      LogException(() => actual.Should().BeEquivalentTo(expected));
    }

    public void Contains(string expected, string actual, string message)
    {
      LogException(() => expected.Should().Contain(actual, "{0}", message));
    }

    public void True(bool condition)
    {
      LogException(() => condition.Should().BeTrue());
    }

    public void True(bool condition, string message)
    {
      LogException(() => condition.Should().BeTrue("{0}", message));
    }

    public void False(bool condition)
    {
      LogException(() => condition.Should().BeFalse());
    }

    public void False(bool condition, string message)
    {
      LogException(() => condition.Should().BeFalse("{0}", message));
    }

    public void Alike<T>(T expected, T actual)
    {
      LogException(() => XAssert.Alike(expected, actual));
    }

    public void IsInstanceOf<T>(object o)
    {
      LogException(() => o.Should().BeOfType<T>());
    }

    public void IsAssignableTo<T>(object o)
    {
      LogException(() => o.Should().BeAssignableTo<T>());
    }

    public void Null(object o)
    {
      LogException(() => o.Should().BeNull());
    }

    public void Same<T>(T expected, T other)
    {
      LogException(() => other.Should().BeSameAs(expected));
    }

    public void NoStaticFields(Assembly assembly)
    {
      LogException(() => XAssert.NoStaticFields(assembly));
    }

    public void NoReference(Assembly assembly1, Assembly assembly2)
    {
      LogException(() => XAssert.NoReference(assembly1, assembly2));
    }

    public void NoReference(Assembly assembly1, Type type)
    {
      LogException(() => XAssert.NoReference(assembly1, type));
    }

    public void NoNonPublicEvents(Assembly assembly)
    {
      LogException(() => XAssert.NoNonPublicEvents(assembly));
    }

    public void SingleConstructor(Assembly assembly)
    {
      LogException(() => XAssert.SingleConstructor(assembly));
    }

    internal void LogException(Action action)
    {
      try
      {
        action.Invoke();
      }
      catch (Exception e)
      {
        _exceptions.Add(AssertionFailed.With(e, _assertionNumber));
      }

      _assertionNumber++;
    }

    public void NotNull<T>(T obj) where T : class
    {
      LogException(() => obj.Should().NotBeNull());
    }

    public void NotEqual<T>(T reference, T actual)
    {
      LogException(() => actual.Should().NotBe(reference));
    }
  }
}