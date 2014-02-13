using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using System.Diagnostics;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public class AssertionRecorder
  {
    private int assertionNumber = 1;
    public List<AssertionFailed> _exceptions = new List<AssertionFailed>();

    internal void AssertTrue()
    {
      XAssert.Equal(0, _exceptions.Count, ExtractMessagesFromAll(_exceptions));
    }

    private string ExtractMessagesFromAll(List<AssertionFailed> _failedAssertions)
    {
      string result = "the following assertions shouldn't have failed: " + Environment.NewLine;
      foreach (var failedAssertion in _failedAssertions)
      {
        result += failedAssertion.Header();
      }

      foreach (var failedAssertion in _failedAssertions)
      {
        result += failedAssertion + Environment.NewLine;
      }

      return result;
    }

    public void Equal<T>(T expected, T actual)
    {
      LogException(() => XAssert.Equal(expected, actual));
    }

    public void Contains(string expected, string actual)
    {
      LogException(() => expected.Should().Contain(actual));
    }

    public void Equal<T>(T expected, T actual, string message)
    {
      LogException(() => XAssert.Equal(expected, actual, message));
    }

    public void Contains(string expected, string actual, string message)
    {
      LogException(() => expected.Should().Contain(actual, message));
    }

    public void True(bool condition)
    {
      LogException(() => condition.Should().BeTrue());
    }

    public void True(bool condition, string message)
    {
      LogException(() => condition.Should().BeTrue(message));
    }

    public void False(bool condition)
    {
      LogException(() => condition.Should().BeFalse());
    }

    public void False(bool condition, string message)
    {
      LogException(() => condition.Should().BeFalse(message));
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

    internal void LogException(Action action)
    {
      try
      {
        action.Invoke();
      }
      catch (Exception e)
      {
        _exceptions.Add(AssertionFailed.With(e, assertionNumber));
      }
      
      assertionNumber++;
    }
  }

  public class AssertionFailed
  {
    private Exception e;
    private int assertionNumber;

    public AssertionFailed(Exception e, int assertionNumber)
    {
      this.e = e;
      this.assertionNumber = assertionNumber;
    }

    public static AssertionFailed With(Exception e, int assertionNumber)
    {
      return new AssertionFailed(e, assertionNumber);
    }

    public string Header()
    {
      return "Assertion no. " + assertionNumber + " failed: " + e.Message + " " + Environment.NewLine;
    }

    public override string ToString()
    {
      return e.ToString();
    }
  }
}
