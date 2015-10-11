using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Core.SequenceChecking;
using NSubstitute.Exceptions;

namespace TddEbook.TddToolkit.NSubstitute
{
  public class XReceived
  {
    public static void Only(Action action)
    {
      new SequenceExclusiveAssertion().Assert(SubstitutionContext.Current.RunQuery(action));
    }
  }

  public class SequenceExclusiveAssertion
  {
    public void Assert(IQueryResults queryResult)
    {
      var querySpec = QuerySpecificationFrom(queryResult);
      var allReceivedCalls = AllCallsExceptPropertyGettersReceivedByTargetsOf(querySpec);

      var callsSpecifiedButNotReceived = GetCallsExpectedButNoReceived(querySpec, allReceivedCalls);
      var callsReceivedButNotSpecified = GetCallsReceivedButNotExpected(querySpec, allReceivedCalls);

      if (callsSpecifiedButNotReceived.Any() || callsReceivedButNotSpecified.Any())
      {
        throw new CallSequenceNotFoundException(GetExceptionMessage(querySpec, allReceivedCalls,
          callsSpecifiedButNotReceived, callsReceivedButNotSpecified));
      }
    }

    private ICall[] AllCallsExceptPropertyGettersReceivedByTargetsOf(CallSpecAndTarget[] querySpec)
    {
      var allUniqueTargets = querySpec.Select(s => s.Target).Distinct();
      var allReceivedCalls = allUniqueTargets.SelectMany(target => target.ReceivedCalls());
      return allReceivedCalls.Where(x => this.IsNotPropertyGetterCall(x.GetMethodInfo())).ToArray();
    }

    private CallSpecAndTarget[] QuerySpecificationFrom(IQueryResults queryResult)
    {
      return
        queryResult.QuerySpecification()
          .Where(x => this.IsNotPropertyGetterCall(x.CallSpecification.GetMethodInfo()))
          .ToArray();
    }

    private static bool Matches(ICall call, CallSpecAndTarget specAndTarget)
    {
      if (object.ReferenceEquals(call.Target(), specAndTarget.Target))
        return specAndTarget.CallSpecification.IsSatisfiedBy(call);
      return false;
    }

    private bool IsNotPropertyGetterCall(MethodInfo methodInfo)
    {
      return methodInfo.GetPropertyFromGetterCallOrNull() == null;
    }

    private static string GetExceptionMessage(
      CallSpecAndTarget[] querySpec, ICall[] receivedCalls,
      CallSpecAndTarget[] callsSpecifiedButNotReceived, ICall[] callsReceivedButNotSpecified)
    {
      var sequenceFormatter = new SequenceFormatter("\n    ", querySpec, receivedCalls);

      var sequenceFormatterForUnexpectedAndExcessiveCalls = new SequenceFormatter("\n    ", callsSpecifiedButNotReceived,
        callsReceivedButNotSpecified);

      return string.Format("\nExpected to receive only these calls:\n{0}{1}\n\n"
                           + "Actually received the following calls:\n{0}{2}\n\n"
                           + "Calls expected but not received:\n{0}{3}\n\n"
                           + "Calls received but not expected:\n{0}{4}\n\n"
                           + "{5}\n\n"

        , "\n    "
        , sequenceFormatter.FormatQuery()
        , sequenceFormatter.FormatActualCalls()
        , sequenceFormatterForUnexpectedAndExcessiveCalls.FormatQuery()
        , sequenceFormatterForUnexpectedAndExcessiveCalls.FormatActualCalls()
        , "*** Note: calls to property getters are not considered part of the query. ***");
    }

    private static ICall[] GetCallsReceivedButNotExpected(CallSpecAndTarget[] expectedCalls, ICall[] receivedCalls)
    {
      return DifferenceBetween(receivedCalls, expectedCalls, Matches);
    }

    private static CallSpecAndTarget[] GetCallsExpectedButNoReceived(CallSpecAndTarget[] expectedCalls, ICall[] receivedCalls)
    {
      return DifferenceBetween(expectedCalls, receivedCalls, (call, spec) => Matches(spec, call));
    }

    private static T2[] DifferenceBetween<T1, T2>(
      IEnumerable<T2> collection1,
      IEnumerable<T1> collection2, 
      Func<T2, T1, bool> matchCriteria) where T1 : class where T2 : class
    {
      var copyOfCollection2 = collection2.ToList();

      var notMatchedCalls = new List<T2>();

      foreach (var call in collection1)
      {
        var matchingSet2Element = copyOfCollection2.FirstOrDefault(spec => matchCriteria(call, spec));
        if (matchingSet2Element != null)
        {
          copyOfCollection2.Remove(matchingSet2Element);
        }
        else
        {
          notMatchedCalls.Add(call);
        }
      }

      return notMatchedCalls.ToArray();
    }
  }

}
