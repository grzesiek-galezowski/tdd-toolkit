namespace TddEbook.TddToolkit.NSubstitute
{
  public interface EquivalencyAssertion
  {
    void Make(object expected, object actual);
  }
}