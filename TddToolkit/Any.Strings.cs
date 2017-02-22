using System.Globalization;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.Subgenerators;

namespace TddEbook.TddToolkit
{

  public partial class Any
  {
    public static string String()
    {
      return _any.String();
    }

    public static string String(string seed)
    {
      return _any.String(seed);
    }

    public static string LowerCaseString()
    {
      return _any.LowerCaseString();
    }

    public static string UpperCaseString()
    {
      return _any.UpperCaseString();
    }

    public static string LowerCaseAlphaString()
    {
      return _any.LowerCaseAlphaString();
    }

    public static string UpperCaseAlphaString()
    {
      return _any.UpperCaseAlphaString();
    }


    public static string StringMatching(string pattern)
    {
      return _any.StringMatching(pattern);
    }

    public static string StringOfLength(int charactersCount)
    {
      return _any.StringOfLength(charactersCount);
    }

    public static string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return _any.StringOtherThan(alreadyUsedStrings);
    }

    public static string StringNotContaining<T>(params T[] excludedObjects)
    {
      return _any.StringNotContaining(excludedObjects);
    }

    public static string StringNotContaining(params string[] excludedSubstrings)
    {
      return _any.StringNotContaining(excludedSubstrings);
    }

    public static string StringContaining<T>(T obj)
    {
      return _any.StringContaining(obj);
    }

    public static string StringContaining(string str)
    {
      return _any.StringContaining(str);
    }

    public static string AlphaString()
    {
      return _any.AlphaString();
    }

    public static string AlphaString(int maxLength)
    {
      return _any.AlphaString(maxLength);
    }

    public static string Identifier()
    {
      return _any.Identifier();
    }

    public static char AlphaChar()
    {
      return _any.AlphaChar();
    }

    public static char DigitChar()
    {
      return _any.DigitChar();
    }

    public static char Char()
    {
      return _any.Char();
    }

    public static string NumericString(int digitsCount = AllGenerator.Many)
    {
      return _any.NumericString(digitsCount);
    }

    public static char LowerCaseAlphaChar()
    {
      return _any.LowerCaseAlphaChar();
    }

    public static char UpperCaseAlphaChar()
    {
      return _any.UpperCaseAlphaChar();
    }
  }
}
