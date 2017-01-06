namespace TddEbook.TddToolkit.NSubstitute
{
  public class ObjectComparerEquivalencyAssertion : EquivalencyAssertion
  {
    public void Make(object expected, object actual)
    {
      XAssert.Alike(expected, actual);
    }
  }

  public class ObjectComparerNonEquivalencyAssertion : EquivalencyAssertion
  {
    public void Make(object expected, object actual)
    {
      XAssert.NotAlike(expected, actual);
    }
  }
}