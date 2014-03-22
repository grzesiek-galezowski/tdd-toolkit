using FluentAssertions;
using TddEbook.TddToolkit.ImplementationDetails;

namespace TddEbook.TddToolkit
{
  public partial class XAssert
  {
    public static void Alike<T>(T expected, T actual)
    {
      var comparison = ObjectGraph.Comparison();
      var result = comparison.Compare(expected, actual);
      result.ExceededDifferences.Should().BeFalse(result.DifferencesString);
    }

    public static void NotAlike<T>(T expected, T actual)
    {
      var comparison = ObjectGraph.Comparison();
      var result = comparison.Compare(expected, actual);
      result.ExceededDifferences.Should().BeTrue(result.DifferencesString);
    }
  }
}
