using System;
using NUnit.Framework;

namespace TddEbook.TddToolkit.Helpers.NUnit
{
  public class XAssert : Common.XAssert
  {
    public static void Equal<T>(T expected, T actual)
    {
      Assert.AreEqual(expected, actual);
    }

    public static void NotEqual<T>(T expected, T actual)
    {
      Assert.AreNotEqual(expected, actual);
    }

    public static void Same<T>(T expected, T actual)
    {
      Assert.AreSame(expected, actual);
    }

    public static void NotSame<T>(T expected, T actual)
    {
      Assert.AreNotSame(expected, actual);
    }

    public static void Equal<T>(T expected, T actual, string message)
    {
      Assert.AreEqual(expected, actual, message);
    }

    public static void NotEqual<T>(T expected, T actual, string message)
    {
      Assert.AreNotEqual(expected, actual, message);
    }

    public static void Same<T>(T expected, T actual, string message)
    {
      Assert.AreSame(expected, actual, message);
    }

    public static void NotSame<T>(T expected, T actual, string message)
    {
      Assert.AreNotSame(expected, actual, message);
    }

    public static void NotNull<T>(T item)
    {
      Assert.NotNull(item);
    }
  }

}
