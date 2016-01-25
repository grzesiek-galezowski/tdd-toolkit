using System.Globalization;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using TddEbook.TddToolkit.ImplementationDetails;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    public static string String()
    {
      return Generator.Create<string>();
    }
    public static string String(string seed)
    {
      return Generator.Create(seed);
    }

    public static string StringMatching(string pattern)
    {
      var request = new RegularExpressionRequest(pattern);

      var result = RegexGenerator.Create(request, new DummyContext());
      return result.ToString();
    }

    public static string StringOfLength(int charactersCount)
    {
      var result = System.String.Empty;
      while (result.Count() < charactersCount)
      {
        result += String();
      }
      return result.Substring(0, charactersCount);
    }

    public static string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return ValueOtherThan(alreadyUsedStrings);
    }

    public static string StringNotContaining<T>(params T[] excludedObjects)
    {
      return StringNotContaining((from obj in excludedObjects select obj.ToString()).ToArray());
    }

    public static string StringNotContaining(params string[] excludedSubstrings)
    {
      var preprocessedStrings = from str in excludedSubstrings
        where !string.IsNullOrEmpty(str)
        select str;

      string result;
      do
      {
        result = String();
      } while (preprocessedStrings.Any(result.Contains));
      return result;
    }

    public static string StringContaining<T>(T obj)
    {
      return StringContaining(obj.ToString());
    }

    public static string StringContaining(string str)
    {
      return String() + str + String();
    }

    public static string AlphaString()
    {
      return AlphaString(String().Length);
    }

    public static string AlphaString(int maxLength)
    {
      var result = System.String.Empty;
      for (var i = 0; i < maxLength; ++i)
      {
        result += AlphaChar();
      }
      return result;
    }

    public static string Identifier()
    {
      string result = AlphaChar().ToString(CultureInfo.InvariantCulture);
      for (var i = 0; i < 5; ++i)
      {
        result += DigitChar();
        result += AlphaChar();
      }
      return result;
    }

    public static char AlphaChar()
    {
      return Letters.Next();
    }

    public static char DigitChar()
    {
      return Digits.Next();
    }

    public static char Char()
    {
      return Instance<char>();
    }

  }
}
