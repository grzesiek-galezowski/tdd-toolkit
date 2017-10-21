using System;
using System.Globalization;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace TddEbook.TddToolkit.Generators
{
  public class StringGenerator
  {
    private readonly Random _randomGenerator = new Random(Guid.NewGuid().GetHashCode());
    private readonly RegularExpressionGenerator _regexGenerator = new RegularExpressionGenerator();
    private readonly CharGenerator _charGenerator;
    private readonly ValueGenerator _valueGenerator;
    private readonly SpecificTypeObjectGenerator _specificGenerator;

    public StringGenerator(
      CharGenerator charGenerator, 
      ValueGenerator valueGenerator, 
      SpecificTypeObjectGenerator specificGenerator)
    {
      _charGenerator = charGenerator;
      _valueGenerator = valueGenerator;
      _specificGenerator = specificGenerator;
    }

    public string LegalXmlTagName()
    {
      return Identifier();
    }

    public string UrlString()
    {
      return _specificGenerator.Uri().ToString();
    }

    public string Ip()
    {
      return _randomGenerator.Next(256) + "."
             + _randomGenerator.Next(256) + "."
             + _randomGenerator.Next(256) + "."
             + _randomGenerator.Next(256);
    }

    public string String() => _valueGenerator.ValueOf<string>();
    public string String(string seed) => _valueGenerator.ValueOf(seed + "_");

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
      while (result.Length < charactersCount)
      {
        result += String();
      }
      return result.Substring(0, charactersCount);
    }

    public string StringOtherThan(params string[] alreadyUsedStrings) 
      => _valueGenerator.ValueOtherThan(alreadyUsedStrings);

    public string StringNotContaining<T>(params T[] excludedObjects) => 
      StringNotContaining((from obj in excludedObjects select obj.ToString()).ToArray());

    public string StringNotContaining(params string[] excludedSubstrings)
    {
      var preprocessedStrings = from str in excludedSubstrings
        where !string.IsNullOrEmpty(str)
        select str;

      var result = String();
      var found = false;
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
      var result = string.Empty;
      for (var i = 0; i < maxLength; ++i)
      {
        result += _charGenerator.AlphaChar();
      }
      return result;
    }

    public string Identifier()
    {
      string result = _charGenerator.AlphaChar().ToString(CultureInfo.InvariantCulture);
      for (var i = 0; i < 5; ++i)
      {
        result += _charGenerator.DigitChar();
        result += _charGenerator.AlphaChar();
      }
      return result;
    }

    public string NumericString(int digitsCount = AllGenerator.Many) => 
      StringMatching("[1-9][0-9]{" + (digitsCount - 1) + "}");
  }

  
}