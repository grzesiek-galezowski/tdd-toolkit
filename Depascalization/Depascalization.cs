using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace Depascalization
{
  public class Depascalization
  {
    public string Of(string input)
    {
      const string space = " ";
      var currentString = input;
      if (currentString.Length == 0) return currentString;

      var parts = currentString.Split('(');
      currentString = parts[0];

      currentString = Char.ToLower(currentString[0]) + currentString.Substring(1);
      currentString = Regex.Replace(currentString, 
        "[^A-Z][A-Z]", m => m._0() + space + m._1());
      currentString = Regex.Replace(currentString, 
        "[a-z][0-9]", m => m._0() + space + m._1());
      currentString = Regex.Replace(currentString, 
        "[IA][A-Z][^A-Z]", m => m._0() + space + m._1() + m._2());
      currentString = Regex.Replace(currentString, 
        "[ ][A-Z][^B-Z ]", m => m._0() + m._1().ToLower() + m._2());
      currentString = Regex.Replace(currentString, 
        "[ ]A[ ]", m => m._0() + m._1().ToLower() + m._2());
      currentString = Regex.Replace(currentString, 
        "[A-Z][A-Z][a-z]", m => m._0() + space + m._1().ToLower() + m._2());
      currentString = Regex.Replace(currentString, 
        "[A-Z][0-9]", m => m._0() + space + m._1().ToLower());
      return currentString + (parts.Length > 1 ? " (" + parts[1] : string.Empty);
    }

    public string OfNUnitReport(string nunitReport)
    {
      /*
      var doc = new XmlDocument();
      doc.LoadXml(nunitReport);

      foreach (var node in doc.SelectNodes("//span[tag=x]"))
      {
        node.InnerXml = "New Content";
      }
      foreach (var node in doc.SelectNodes("//span[tag=y]"))
      {
        node.InnerXml = "Different Content";
      }*/
      return "not correct";
    }
  }
}