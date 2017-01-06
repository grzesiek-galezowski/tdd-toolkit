namespace TddEbook.TddToolkit.NSubstitute.ImplementationDetails
{
  public class ObjectComparerEquivalencyAssertion : EquivalencyAssertion
  {
    public void Make(object expected, object actual)
    {
      XAssert.Alike(expected, actual);
    }
  }
}