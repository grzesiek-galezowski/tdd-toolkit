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
    }

    public static string String(string seed)
    {
    }

    public static string LowerCaseString()
    {
    }

    public static string UpperCaseString()
    {
    }

    public static string LowerCaseAlphaString()
    {
    }

    public static string UpperCaseAlphaString()
    {
    }


    public static string StringMatching(string pattern)
    {
    }

    public static string StringOfLength(int charactersCount)
    {
    }

    public static string StringOtherThan(params string[] alreadyUsedStrings)
    {
    }

    public static string StringNotContaining<T>(params T[] excludedObjects)
    {
    }

    public static string StringNotContaining(params string[] excludedSubstrings)
    {
    }

    public static string StringContaining<T>(T obj)
    {
    }

    public static string StringContaining(string str)
    {
    }

    public static string AlphaString()
    {
    }

    public static string AlphaString(int maxLength)
    {
    }

    public static string Identifier()
    {
    }

    public static char AlphaChar()
    {
    }

    public static char DigitChar()
    {
    }

    public static char Char()
    {
    }

    {
    }

    public static char LowerCaseAlphaChar()
    {
    }

    public static char UpperCaseAlphaChar()
    {
    }
  }
}
