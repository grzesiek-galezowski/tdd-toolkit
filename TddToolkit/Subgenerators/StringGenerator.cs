using System;
using System.Globalization;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using TddEbook.TddToolkit.ImplementationDetails;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class StringGenerator
  {
    private readonly AllGenerator _allGenerator;
    private readonly Fixture _generator;
    private readonly Random _randomGenerator = new Random(System.Guid.NewGuid().GetHashCode());
    private readonly RegularExpressionGenerator _regexGenerator = new RegularExpressionGenerator();

    public StringGenerator(Fixture generator, AllGenerator allGenerator)
    {
      _generator = generator;
      _allGenerator = allGenerator;
    }

    public string LegalXmlTagName()
    {
      return Identifier();
    }

    public string UrlString()
    {
      return _allGenerator.Uri().ToString();
    }

    public string Ip()
    {
      return _randomGenerator.Next(256) + "."
             + _randomGenerator.Next(256) + "."
             + _randomGenerator.Next(256) + "."
             + _randomGenerator.Next(256);
    }

    public string String() => _allGenerator.ValueOf<string>();
    public string String(string seed) => _generator.Create(seed+"_");
    public string LowerCaseString() => String().ToLower();
    public string UpperCaseString() => String().ToUpper();
    public string LowerCaseAlphaString() => AlphaString().ToLower();
    public string UpperCaseAlphaString() => AlphaString().ToUpper();

    public string StringMatching(string pattern)
    {
      var request = new RegularExpressionRequest(pattern);

      var result = _regexGenerator.Create(request, new DummyContext());
      return result.ToString();
    }

    public string StringOfLength(int charactersCount)
    {
      var result = string.Empty;
      while (result.Count() < charactersCount)
      {
        result += String();
      }
      return result.Substring(0, charactersCount);
    }

    public string StringOtherThan(params string[] alreadyUsedStrings) => _allGenerator.ValueOtherThan(alreadyUsedStrings);

    public string StringNotContaining<T>(params T[] excludedObjects) => 
      StringNotContaining((from obj in excludedObjects select obj.ToString()).ToArray());

    public string StringNotContaining(params string[] excludedSubstrings)
    {
      var preprocessedStrings = from str in excludedSubstrings
        where !string.IsNullOrEmpty(str)
        select str;

      string result = String();
      bool found = false;
      for(int i = 0 ; i < 100 ; ++i)
      {
        result = String();
        if (preprocessedStrings.Any(result.Contains))
        {
          found = true;
          break;
        }
      }
      if (!found)
      {
        foreach (var excludedSubstring in excludedSubstrings.Where(s => s != string.Empty))
        {
          result = result.Replace(excludedSubstring, "");
        }
      }
      return result;
    }

    public string StringContaining<T>(T obj) => 
      StringContaining(obj.ToString());

    public string StringContaining(string str) => 
      String() + str + String();

    public string AlphaString() => 
      AlphaString(String().Length);

    public string AlphaString(int maxLength)
    {
      var result = System.String.Empty;
      for (var i = 0; i < maxLength; ++i)
      {
        result += _allGenerator.AlphaChar();
      }
      return result;
    }

    public string Identifier()
    {
      string result = _allGenerator.AlphaChar().ToString(CultureInfo.InvariantCulture);
      for (var i = 0; i < 5; ++i)
      {
        result += _allGenerator.DigitChar();
        result += _allGenerator.AlphaChar();
      }
      return result;
    }

    public string NumericString(int digitsCount = AllGenerator.Many) => 
      StringMatching("[1-9][0-9]{" + (digitsCount - 1) + "}");
  }
}