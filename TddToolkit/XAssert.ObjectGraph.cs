using FluentAssertions;
using TddEbook.TddToolkit.ImplementationDetails;

namespace TddEbook.TddToolkit
{
  public partial class XAssert
  {
    public static void Alike<T>(T expected, T actual)
    {
      var comparison = ObjectGraph.Comparison();
      comparison.Compare(expected, actual).Should().BeTrue(comparison.DifferencesString);
    }

    public static void NotAlike<T>(T expected, T actual)
    {
      var comparison = ObjectGraph.Comparison();
      comparison.Compare(expected, actual).Should().BeFalse(comparison.DifferencesString);
    }
  }
}
