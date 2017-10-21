using TddEbook.TddToolkit.Generators;
using TddEbook.TddToolkit.Subgenerators;

namespace TddEbook.TddToolkit
{

  public partial class Any
  {
    public static string String()
    {
      return Generate.String();
    }

    public static string String(string seed)
    {
      return Generate.String(seed);
    }

    public static string LowerCaseString()
    {
      return Generate.LowerCaseString();
    }

    public static string UpperCaseString()
    {
      return Generate.UpperCaseString();
    }

    public static string LowerCaseAlphaString()
    {
      return Generate.LowerCaseAlphaString();
    }

    public static string UpperCaseAlphaString()
    {
      return Generate.UpperCaseAlphaString();
    }


    public static string StringMatching(string pattern)
    {
      return Generate.StringMatching(pattern);
    }

    public static string StringOfLength(int charactersCount)
    {
      return Generate.StringOfLength(charactersCount);
    }

    public static string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return Generate.StringOtherThan(alreadyUsedStrings);
    }

    public static string StringNotContaining<T>(params T[] excludedObjects)
    {
      return Generate.StringNotContaining(excludedObjects);
    }

    public static string StringNotContaining(params string[] excludedSubstrings)
    {
      return Generate.StringNotContaining(excludedSubstrings);
    }

    public static string StringContaining<T>(T obj)
    {
      return Generate.StringContaining(obj);
    }

    public static string StringContaining(string str)
    {
      return Generate.StringContaining(str);
    }

    public static string AlphaString()
    {
      return Generate.AlphaString();
    }

    public static string AlphaString(int maxLength)
    {
      return Generate.AlphaString(maxLength);
    }

    public static string Identifier()
    {
      return Generate.Identifier();
    }

    public static char AlphaChar()
    {
      return Generate.AlphaChar();
    }

    public static char DigitChar()
    {
      return Generate.DigitChar();
    }

    public static char Char()
    {
      return Generate.Char();
    }

    public static string NumericString(int digitsCount = AllGenerator.Many)
    {
      return Generate.NumericString(digitsCount);
    }

    public static char LowerCaseAlphaChar()
    {
      return Generate.LowerCaseAlphaChar();
    }

    public static char UpperCaseAlphaChar()
    {
      return Generate.UpperCaseAlphaChar();
    }
  }
}
