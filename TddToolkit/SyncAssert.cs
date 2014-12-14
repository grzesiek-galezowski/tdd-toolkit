using System;
using NSubstitute;

namespace TddEbook.TddToolkit
{
  public partial class XAssert
  {
    public static void Synchronizes<T>(T wrappingObject, Action<T> callToCheck, LockAssertions lockAssertions,
                                             T wrappedObjectMock) where T : class
    {
      LockShouldBeReleasedAfterACall(wrappingObject, wrappedObjectMock, callToCheck, lockAssertions);
      LockShouldBeReleasedWhenCallThrowsException(lockAssertions, wrappingObject, wrappedObjectMock, callToCheck);
    }

    public static void Synchronizes<T, TReturn>(T wrappingObject, Func<T, TReturn> callToCheck, LockAssertions lockAssertions, T wrappedObjectMock) where T : class
    {
      LockShouldBeReleasedAfterACall(wrappingObject, wrappedObjectMock, callToCheck, lockAssertions);
      LockShouldBeReleasedWhenCallThrowsException(lockAssertions, wrappingObject, wrappedObjectMock, t => callToCheck(t));
    }

    private static void LockShouldBeReleasedWhenCallThrowsException<T>(LockAssertions lockAssertions,
                                                                       T wrappingObject, T wrappedObjectMock,
                                                                       Action<T> callToCheck) where T : class
    {
      try
      {
        wrappedObjectMock.When(callToCheck).Do(_ =>
        {
          lockAssertions.AssertLocked();
          throw new Exception();
        });

        callToCheck(wrappingObject);
        throw new Exception("The specified method was probably not called by the proxy with exactly the same arguments it received");
      }
      catch
      {
        lockAssertions.AssertUnlocked();
      }
    }

    private static void LockShouldBeReleasedAfterACall<T>(T wrappingObject, T wrappedObjectMock, Action<T> callToCheck,
                                                          LockAssertions lockAssertions)
      where T : class
    {
      wrappedObjectMock.When(callToCheck).Do(_ => lockAssertions.AssertLocked());
      lockAssertions.AssertUnlocked();
      callToCheck(wrappingObject);
      lockAssertions.AssertUnlocked();
      callToCheck(wrappedObjectMock.Received(1));
    }

    private static void LockShouldBeReleasedAfterACall<T, TReturn>(T wrappingObject, T wrappedObjectMock,
                                                                   Func<T, TReturn> callToCheck,
                                                                   LockAssertions lockAssertions)
      where T : class
    {
      var cannedResult = Any.Instance<TReturn>();
      callToCheck(wrappedObjectMock).Returns((ci) =>
      {
        lockAssertions.AssertLocked();
        return cannedResult;
      });

      lockAssertions.AssertUnlocked();
      var actualResult = callToCheck(wrappingObject);
      lockAssertions.AssertUnlocked();
      Equal(cannedResult, actualResult, "The specified method was probably not called by the proxy with exactly the same arguments it received or it did not return the value obtained from wrapped call");
    }
  }

  public static class SyncAssert
  {
    public static void AssertSynchronizes<T>(this T wrappingObject, Action<T> callToCheck, LockAssertions lockAssertions, T wrappedObjectMock) where T : class
    {
      XAssert.Synchronizes(wrappingObject, callToCheck, lockAssertions, wrappedObjectMock);
    }

    public static void AssertSynchronizes<T, TReturn>(this T wrappingObject, Func<T, TReturn> callToCheck, LockAssertions lockAssertions, T wrappedObjectMock) where T : class
    {
      XAssert.Synchronizes(wrappingObject, callToCheck, lockAssertions, wrappedObjectMock);
    }
  }
}