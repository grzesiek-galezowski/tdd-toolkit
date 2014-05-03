using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentAssertions;

namespace TddEbook.TddToolkit.ChainedAssertions
{
  public static class ChainedAssertions
  {
    public static T AssertSameAs<T>(this T instance, T otherInstance, string message)
    {
      XAssert.Same(instance, otherInstance, message);
      return instance;
    }

    public static T AssertNotSameAs<T>(this T instance, T otherInstance, string message)
    {
      XAssert.NotSame(instance, otherInstance, message);
      return instance;
    }

    public static T AssertContains<T, U>(this T instance, U containedInstance, string message) where T : IEnumerable<U>
    {
      instance.Should().Contain(containedInstance);
      return instance;
    }

    public static T AssertContains<T, U>(this T instance, Expression<Func<U, bool>> predicate, string message) where T : IEnumerable<U>
    {
      instance.Should().Contain(predicate, message);
      return instance;
    }


    public static T AssertDoesNotContain<T, U>(this T instance, U containedInstance, string message) where T : IEnumerable<U>
    {
      instance.Should().NotContain(containedInstance);
      return instance;
    }

    public static T AssertDoesNotContain<T, U>(this T instance, Expression<Func<U, bool>> predicate, string message) where T : IEnumerable<U>
    {
      instance.Should().NotContain(predicate, message);
      return instance;
    }

    public static T AssertIsEmpty<T>(this T instance, string message) where T : IEnumerable
    {
      instance.Should().BeEmpty(message);
      return instance;
    }

    public static T AssertIsNotEmpty<T>(this T instance, string message) where T : IEnumerable
    {
      instance.Should().NotBeEmpty(message);
      return instance;
    }

    public static T AssertIsInAcendingOrder<T>(this T instance, string message) where T : IEnumerable
    {
      instance.Should().BeInAscendingOrder(message);
      return instance;
    }

    public static T AssertIsInDescendingOrder<T>(this T instance, string message) where T : IEnumerable
    {
      instance.Should().BeInDescendingOrder(message);
      return instance;
    }

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

    public static int AssertPositive(this int instance, string message)
    {
      instance.Should().BePositive();
      return instance;
    }

    public static long AssertPositive(this long instance, string message)
    {
      instance.Should().BePositive();
      return instance;
    }

    public static double AssertPositive(this double instance, string message)
    {
      instance.Should().BePositive();
      return instance;
    }

    public static int AssertNegative(this int instance, string message)
    {
      instance.Should().BeNegative();
      return instance;
    }

    public static long AssertNegative(this long instance, string message)
    {
      instance.Should().BeNegative();
      return instance;
    }

    public static double AssertNegative(this double instance, string message)
    {
      instance.Should().BeNegative();
      return instance;
    }

    



  }
}