using System;

namespace TddEbook.TddToolkit.ImplementationDetails.Common
{
  public class AssertionFailed
  {
    private readonly Exception _e;
    private readonly int _assertionNumber;

    public AssertionFailed(Exception e, int assertionNumber)
    {
      _e = e;
      _assertionNumber = assertionNumber;
    }

    public static AssertionFailed With(Exception e, int assertionNumber)
    {
      return new AssertionFailed(e, assertionNumber);
    }

    public string Header()
    {
      return String.Format(
        "Assertion no. {0} failed: {1} {2}", 
        _assertionNumber, _e.Message, Environment.NewLine);
    }

    public override string ToString()
    {
      return _e.ToString();
    }
  }
}
