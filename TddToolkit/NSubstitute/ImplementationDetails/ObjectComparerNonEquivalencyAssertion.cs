namespace TddEbook.TddToolkit.NSubstitute.ImplementationDetails
{
  public class ObjectComparerNonEquivalencyAssertion : EquivalencyAssertion
  {
    public void Make(object expected, object actual)
    {
      XAssert.NotAlike(expected, actual);
    }
  }
}