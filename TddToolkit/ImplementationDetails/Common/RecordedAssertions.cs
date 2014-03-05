using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.ImplementationDetails.Common
{
  public class RecordedAssertions
  {
    public static void True(bool condition, string message, IConstraintsViolations errors)
    {
      if (!condition)
      {
        errors.Add(message);
      }
    }

    public static void False(bool condition, string message, ConstraintsViolations errors)
    {
      True(!condition, message, errors);
    }

    public static void NotEqual<T>(T i, T i2, string message, ConstraintsViolations errors)
    {
      False(Equals(i, i2), message, errors);
    }

    public static void Equal<T>(T i, T i2, string message, ConstraintsViolations errors)
    {
      True(Equals(i, i2), message, errors);
    }
  }
}