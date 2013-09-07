namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class NestingLimit
  {
    private readonly int _limit;
    private int _nesting;

    private NestingLimit(int limit)
    {
      _limit = limit;
    }

    public static NestingLimit Of(int limit)
    {
      return new NestingLimit(limit);
    }

    public void AddNesting()
    {
      _nesting++;
    }

    public bool IsReached()
    {
      return _nesting > _limit;
    }

    public void RemoveNesting()
    {
      _nesting--;
    }
  }
}