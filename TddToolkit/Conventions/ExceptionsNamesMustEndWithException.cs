using System;
using System.Linq;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace TddEbook.TddToolkit.Conventions
{
  public class ExceptionsNamesMustEndWithException : IConvention<Types>
  {
    public void Execute(Types data, IConventionResultContext result)
    {
      result.IsSymmetric(
        "All types inheriting from exception must end with 'Exception'",
        data
          .Where(t => t.IsSubclassOf(typeof(Exception)))
          .Where(t => !t.Name.EndsWith("Exception")),
        "All types ending with 'Exception' must inherit from Exception",
        data
          .Where(t => t.Name.EndsWith("Exception"))
          .Where(t => !t.IsSubclassOf(typeof(Exception))));
    }

    public string ConventionReason { get; } = "By convention, all exception classes should have names ending with 'Exception'";
  }
}