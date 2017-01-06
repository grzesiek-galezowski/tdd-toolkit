using System.Linq;
using System.Text;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Core.SequenceChecking;
using NSubstitute.Exceptions;

namespace TddEbook.TddToolkit.NSubstitute
{
  public static class Extensions
  {
    public static T ReceivedNothing<T>(this T substitute) where T : class
    {
      if (substitute.ReceivedCalls().Count() != 0)
      {
        var message = GetMessage(substitute);
        throw new CallSequenceNotFoundException(message);
      }
      return substitute;
    }

    private static string GetMessage<T>(T substitute) where T : class
    {
      var formatter = new SequenceFormatter("\n    ", new CallSpecAndTarget[] {}, substitute.ReceivedCalls().ToArray());
      var message = string.Format(
        "\nExpected to receive *no calls*.\n"
        + "Actually received the following calls:\n{0}{1}\n\n"
        , "\n    "
        , formatter.FormatActualCalls());
      return message;
    }
  }
}
