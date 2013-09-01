using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using TddEbook.TddToolkit.ImplementationDetails;

namespace TddEbook.TddToolkit
{
  public static partial class Any
  {
    public static string String()
    {
      return _generator.Create<string>();
    }

    public static string StringMatching(string pattern)
    {
      var request = new RegularExpressionRequest(pattern);

      var result = _regexGenerator.Create(request, new DummyContext());
      return result.ToString();
    }

    public static string StringOfLength(int charactersCount)
    {
      var result = string.Empty;
      while (result.Count() < charactersCount)
      {
        result += Any.String();
      }
      return result.Substring(0, charactersCount);
    }

    public static string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return Any.ValueOtherThan(alreadyUsedStrings);
    }

    public static string StringNotContaining(params string[] excludedSubstrings)
    {
      var result = Any.String();
      do
      {
        result = Any.String();
      } while (excludedSubstrings.Any(result.Contains));
      return result;
    }

    public static string StringContaining(string str)
    {
      return Any.String() + str + Any.String();
    }

    public static string AlphaString()
    {
      return AlphaString(Any.String().Length);
    }

    public static string AlphaString(int maxLength)
    {
      var result = string.Empty;
      for (var i = 0; i < maxLength; ++i)
      {
        result += Any.AlphaChar();
      }
      return result;
    }

    public static string Identifier()
    {
      string result = Any.AlphaChar().ToString();
      for (var i = 0; i < 5; ++i)
      {
        result += Any.DigitChar();
        result += Any.AlphaChar();
      }
      return result;
    }

    public static char AlphaChar()
    {
      return _letters.Next();
    }

    static char DigitChar()
    {
      return _digits.Next();
    }

  }
}
