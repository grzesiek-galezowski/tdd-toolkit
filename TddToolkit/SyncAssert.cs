using System;
using System.Reflection;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Exceptions;

namespace TddEbook.TddToolkit
{
  public partial class XAssert
  {
    public static void Synchronizes<T>(T wrappingObject, Action<T> callToCheck, LockAssertions lockAssertions,
                                             T wrappedObjectMock) where T : class
    {
      NSubstituteIsInCorrectVersion(wrappedObjectMock);
      LockShouldBeReleasedAfterACall(wrappingObject, wrappedObjectMock, callToCheck, lockAssertions);
      LockShouldBeReleasedWhenCallThrowsException(lockAssertions, wrappingObject, wrappedObjectMock, callToCheck);
    }

    public static void Synchronizes<T, TReturn>(T wrappingObject, Func<T, TReturn> callToCheck, LockAssertions lockAssertions, T wrappedObjectMock) where T : class
    {
      NSubstituteIsInCorrectVersion(wrappedObjectMock);
      LockShouldBeReleasedAfterACall(wrappingObject, wrappedObjectMock, callToCheck, lockAssertions);
      LockShouldBeReleasedWhenCallThrowsException(lockAssertions, wrappingObject, wrappedObjectMock, t => callToCheck(t));
    }

    private static void NSubstituteIsInCorrectVersion<T>(T wrappedObjectMock) where T : class
    {
      try
      {
        wrappedObjectMock.ClearReceivedCalls();
      }
      catch (NotASubstituteException e)
      {
        AssertReferencedAndLoadedVersionsOfNSubstituteAreTheSame();
        throw e;
      }
    }

    private static void AssertReferencedAndLoadedVersionsOfNSubstituteAreTheSame()
    {
      var checkedAssemblyName = "NSubstitute";
      ReferencedAndLoadedAssemblyVersionsAreTheSame(checkedAssemblyName);
    }

    public static void ReferencedAndLoadedAssemblyVersionsAreTheSame(string checkedAssemblyName)
    {
      foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
      {
        if (assemblyName.Name.StartsWith(checkedAssemblyName))
        {
          var referencedAssemblyInfo = Assembly.Load(assemblyName).GetName();
          var referencedAssemblyShortName = referencedAssemblyInfo.Name;

          if (referencedAssemblyShortName.Equals(checkedAssemblyName))
          {
            foreach (var loadedAssembly in AppDomain.CurrentDomain.GetAssemblies())
            {
              var loadedAssemblyInfo = loadedAssembly.GetName();
              if (referencedAssemblyShortName.Equals(loadedAssemblyInfo.Name))
              {
                loadedAssemblyInfo.Version.Should().Be(referencedAssemblyInfo.Version,
                  "this is the version number of "+ checkedAssemblyName +" assembly referenced internally by Tdd-Toolkit " +
                  "and it should match the version of assembly loaded at runtime (currently, this is not the case, " +
                  "which means your tests are using an external "+ checkedAssemblyName + " dll with version different than the one needed by Tdd-Toolkit" +
                  " - please update your "+ checkedAssemblyName + " assembly to version "+ referencedAssemblyInfo.Version + ")");
              }
            }
          }
        }
      }
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
        throw new Exception(
          "The specified method was probably not called by the proxy with exactly the same arguments it received");
      }
      catch
      {
        lockAssertions.AssertUnlocked();
      }
      finally
      {
        wrappedObjectMock.ClearReceivedCalls();
      }
    }

    private static void LockShouldBeReleasedAfterACall<T>(T wrappingObject, T wrappedObjectMock, Action<T> callToCheck,
                                                          LockAssertions lockAssertions)
      where T : class
    {
      try
      {

        wrappedObjectMock.When(callToCheck).Do(_ => lockAssertions.AssertLocked());
        lockAssertions.AssertUnlocked();
        callToCheck(wrappingObject);
        lockAssertions.AssertUnlocked();
        callToCheck(wrappedObjectMock.Received(1));
      }
      finally
      {
        wrappedObjectMock.ClearReceivedCalls();
      }
    }

    private static void LockShouldBeReleasedAfterACall<T, TReturn>(T wrappingObject, T wrappedObjectMock,
                                                                   Func<T, TReturn> callToCheck,
                                                                   LockAssertions lockAssertions)
      where T : class
    {
      try
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
        Equal(cannedResult, actualResult,
              "The specified method was probably not called by the proxy with exactly the same arguments it received or it did not return the value obtained from wrapped call");
      }
      finally
      { 
        wrappedObjectMock.ClearReceivedCalls();
      }
    }
  }

  public static class SyncAssert
  {
    public static void AssertSynchronizes<T>(this T wrappingObject, T wrappedObjectMock, Action<T> callToCheck, LockAssertions lockAssertions) where T : class
    {
      XAssert.Synchronizes(wrappingObject, callToCheck, lockAssertions, wrappedObjectMock);
    }

    public static void AssertSynchronizes<T, TReturn>(this T wrappingObject, Func<T, TReturn> callToCheck, LockAssertions lockAssertions, T wrappedObjectMock) where T : class
    {
      XAssert.Synchronizes(wrappingObject, callToCheck, lockAssertions, wrappedObjectMock);
    }
  }
}