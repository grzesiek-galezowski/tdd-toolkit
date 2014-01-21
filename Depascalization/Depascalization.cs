using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DePascalizationSpecification
{
  public class Depascalization
  {
    public string Of(string input)
    {
      var currentString = input;
      if (currentString.Length == 0) return currentString;

      var parts = currentString.Split('(');
      currentString = parts[0];

      currentString = Char.ToLower(currentString[0]) + currentString.Substring(1);
      currentString = Regex.Replace(currentString, "[^A-Z][A-Z]", new MatchEvaluator(m => { return m.Value[0]  + " " + m.Value[1]; }));
      currentString = Regex.Replace(currentString, "[a-z][0-9]", new MatchEvaluator(m => { return m.Value[0] + " " + m.Value[1]; }));
      currentString = Regex.Replace(currentString, "[IA][A-Z][^A-Z]", new MatchEvaluator(m => { return m.Value[0] + " " + m.Value[1] + m.Value[2]; }));
      currentString = Regex.Replace(currentString, "[ ][A-Z][^B-Z ]", new MatchEvaluator(m => { return m.Value[0].ToString() + Char.ToLower(m.Value[1]) + m.Value[2]; }));
      currentString = Regex.Replace(currentString, "[ ]A[ ]", new MatchEvaluator(m => { return m.Value[0].ToString() + Char.ToLower(m.Value[1]) + m.Value[2]; }));
      currentString = Regex.Replace(currentString, "[A-Z][A-Z][a-z]", new MatchEvaluator(m => { return m.Value[0].ToString() + " " + Char.ToLower(m.Value[1]) + m.Value[2]; }));
      currentString = Regex.Replace(currentString, "[A-Z][0-9]", new MatchEvaluator(m => { return m.Value[0].ToString() + " " + Char.ToLower(m.Value[1]); }));
      //currentString = currentString.ToLower();
      return currentString + (parts.Length > 1 ? " (" + parts[1] : string.Empty);
    }
  }
}
