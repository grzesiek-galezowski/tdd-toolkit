using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

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
      var element = XDocument.Parse(nunitReport);

      var namespacesAndFixtures = from e in element.Descendants("test-suite")
                       where e.Attribute("type").Value == "Namespace" || e.Attribute("type").Value == "TestFixture"
                       select e;
      
      foreach(var namespaceElement in namespacesAndFixtures)
      {
        namespaceElement.SetAttributeValue("name", namespaceElement.Attribute("name").Value.Replace("Specification", " Specification"));
      }

      var testCases = from e in element.Descendants("test-case") select e;

      foreach (var testCase in testCases)
      {
        testCase.SetAttributeValue("name", testCase.Attribute("name").Value.Replace(".", " "));
      }


      return element.Declaration + Environment.NewLine + element.ToString();
    }
  }
}