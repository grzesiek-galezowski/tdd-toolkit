using System.Threading;
using FluentAssertions;

namespace TddEbook.TddToolkit.LockSpecificAssertions
{
  public class ReadLockSlimAssertions : LockAssertions
  {
    private readonly ReaderWriterLockSlim _aLock;

    public ReadLockSlimAssertions(ReaderWriterLockSlim aLock)
    {
      _aLock = aLock;
    }

    public void AssertUnlocked()
    {
      _aLock.IsReadLockHeld.Should().BeFalse("Expected read lock not being held at this moment, but it is!");
      AssertAlternativeLocksNotHeld();
    }

    private void AssertAlternativeLocksNotHeld()
    {
      _aLock.IsWriteLockHeld.Should().BeFalse("Expected write lock being held at all, but it is!");
    }

    public void AssertLocked()
    {
      _aLock.IsReadLockHeld.Should().BeTrue("Expected read lock being held, but it is not!");
      AssertAlternativeLocksNotHeld();
    }
  }
}