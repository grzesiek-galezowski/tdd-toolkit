using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public class AssertionRecorder
  {
    public List<Exception> _exceptions = new List<Exception>();

    internal void AssertTrue()
    {
      XAssert.Equal(0, _exceptions.Count, ExtractMessagesFromAll(_exceptions));
    }

    private string ExtractMessagesFromAll(List<Exception> _exceptions)
    {
      string result = "the following assertions shouldn't have failed: " + Environment.NewLine;
      foreach (var e in _exceptions)
      {
        result += e.Message + Environment.NewLine;
      }

      foreach (var e in _exceptions)
      {
        result += e + Environment.NewLine;
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



    public void LogException(Action action)
    {
      try
      {
        action.Invoke();
      }
      catch (Exception e)
      {
        _exceptions.Add(e);
      }
    }
  }
}
