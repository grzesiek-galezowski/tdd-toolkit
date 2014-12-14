using System.Threading;
using TddEbook.TddToolkit.LockSpecificAssertions;

namespace TddEbook.TddToolkit
{
  public static class Blocking
  {
    public static LockAssertions ReadOn(ReaderWriterLockSlim aLock)
    {
      return new ReadLockSlimAssertions(aLock);
    }

    public static LockAssertions WriteOn(ReaderWriterLockSlim aLock)
    {
      return new WriteLockSlimAssertions(aLock);
    }

    public static LockAssertions MonitorOn(object aLock)
    {
      return new MonitorAssertions(aLock);
    }
  }
}