using TddEbook.TddToolkit.ImplementationDetails;

namespace TddEbook.TddToolkit
{
  public class Are
  {
    public static bool Alike<T>(T expected, T actual)
    {
      var comparison = ObjectGraph.Comparison();
      return comparison.Compare(expected, actual);
    }
  }
}
