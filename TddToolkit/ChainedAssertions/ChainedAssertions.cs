using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace TddEbook.TddToolkit.ChainedAssertions
{
  public static class ChainedAssertions
  {
    public static T AssertNotNull<T>(this T instance, string message)
    {
      XAssert.NotNull(instance, message);
      return instance;
    }

    public static T AssertEqualTo<T>(this T instance, T expected, string message)
    {
      XAssert.Equal(expected, instance, message);
      return instance;
    }

    public static T AssertGreaterThan<T>(this T instance, IComparable<T> anotherInstance, string message)
    {
      anotherInstance.Should().BeLessThan(instance, message);
      return instance;
    }

    public static T AssertGreaterOrEqualTo<T>(this T instance, IComparable<T> anotherInstance, string message)
    {
      anotherInstance.Should().BeLessOrEqualTo(instance, message);
      return instance;
    }
    
    public static T AssertLessThan<T>(this T instance, IComparable<T> anotherInstance, string message)
    {
      anotherInstance.Should().BeGreaterThan(instance, message);
      return instance;
    }

    public static T AssertLessOrEqualTo<T>(this T instance, IComparable<T> anotherInstance, string message)
    {
      anotherInstance.Should().BeGreaterOrEqualTo(instance, message);
      return instance;
    }

  }
}