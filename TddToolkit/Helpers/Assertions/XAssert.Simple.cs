using FluentAssertions;

namespace TddEbook.TddToolkit.Helpers.Assertions
{
  public partial class XAssert
  {
    public static void Equal<T>(T expected, T actual)
    {
      actual.Should().Be(expected);
    }

    public static void NotEqual<T>(T expected, T actual)
    {
      actual.Should().NotBe(expected);
    }

    public static void Same<T>(T expected, T actual)
    {
      actual.Should().BeSameAs(expected);
    }

    public static void NotSame<T>(T expected, T actual)
    {
      actual.Should().NotBeSameAs(expected);
    }

    public static void Equal<T>(T expected, T actual, string message)
    {
      actual.Should().Be(expected, message);
    }

    public static void NotEqual<T>(T expected, T actual, string message)
    {
      actual.Should().NotBe(expected, message);
    }

    public static void Same<T>(T expected, T actual, string message)
    {
      actual.Should().BeSameAs(expected, message);
    }

    public static void NotSame<T>(T expected, T actual, string message)
    {
      actual.Should().NotBeSameAs(expected, message);
    }

    public static void NotNull<T>(T item)
    {
      item.Should().NotBeNull();
    }

    public static void NotNull<T>(T item, string message)
    {
      item.Should().NotBeNull(message);
    }
  }
}
