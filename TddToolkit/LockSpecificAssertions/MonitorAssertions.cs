using System;
using System.Threading;

namespace TddEbook.TddToolkit.LockSpecificAssertions
{
  public class MonitorAssertions : LockAssertions
  {
    private readonly object _aLock;

    public MonitorAssertions(object aLock)
    {
      _aLock = aLock;
    }

    public void AssertUnlocked()
    {
      try
      {
        Monitor.Exit(_aLock);
        throw new Exception("Expected lock not being held, but it is!");
      }
      catch (SynchronizationLockException)
      {
      }
    }

    public void AssertLocked()
    {
      Monitor.Exit(_aLock);
      Monitor.Enter(_aLock);
    }
  }
}