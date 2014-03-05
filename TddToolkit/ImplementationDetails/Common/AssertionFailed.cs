using System;

namespace TddEbook.TddToolkit.ImplementationDetails.Common
{
  public class AssertionFailed
  {
    private readonly Exception e;
    private readonly int assertionNumber;

    public AssertionFailed(Exception e, int assertionNumber)
    {
      this.e = e;
      this.assertionNumber = assertionNumber;
    }

    public static AssertionFailed With(Exception e, int assertionNumber)
    {
      return new AssertionFailed(e, assertionNumber);
    }

    public string Header()
    {
      return String.Format(
        "Assertion no. {0} failed: {1} {2}", 
        assertionNumber, e.Message, Environment.NewLine);
    }

    public override string ToString()
    {
      return e.ToString();
    }
  }
}
