using System.Threading;
using FluentAssertions;

namespace TddEbook.TddToolkit.LockSpecificAssertions
{
  public class WriteLockSlimAssertions : LockAssertions
  {
    private readonly ReaderWriterLockSlim _aLock;

    public WriteLockSlimAssertions(ReaderWriterLockSlim aLock)
    {
      _aLock = aLock;
    }

    public void AssertUnlocked()
    {
      _aLock.IsWriteLockHeld.Should().BeFalse("Expected write lock not being held at this moment, but it is!");
      AssertAlternativeLocksNotHeld();
    }

    private void AssertAlternativeLocksNotHeld()
    {
      _aLock.IsReadLockHeld.Should().BeFalse("Expected read lock not being held at all, but it is!");
    }

    public void AssertLocked()
    {
      _aLock.IsWriteLockHeld.Should().BeTrue("Expected write lock being held, but it is not!");
      AssertAlternativeLocksNotHeld();
    }
  }
}