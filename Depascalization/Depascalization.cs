using System;
using System.Collections.Generic;
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
      var document = XDocument.Parse(nunitReport);

      var namespacesAndFixtures = ExtractNamespacesAndFixtures(document);

      foreach(var namespaceElement in namespacesAndFixtures)
      {
        namespaceElement.HumanizeName();
      }

      var testCases = ExtractTestCasesFrom(document);

      foreach (var testCase in testCases)
      {
        testCase.HumanizeName();
      }

      return document.Declaration + Environment.NewLine + document;
    }

    private static IEnumerable<TestCase> ExtractTestCasesFrom(XContainer document)
    {
      return from e in document.Descendants("test-case") select new TestCase(e);
    }

    private static IEnumerable<NamespaceOrFixture> ExtractNamespacesAndFixtures(XDocument element)
    {
      var namespaceAndFixture = new[] {"Namespace", "TestFixture"};
      var namespacesAndFixtures = from e in element.Descendants("test-suite")
                                  where e.IsValueOfItsAttribute("type", oneOf: namespaceAndFixture)
                                  select new NamespaceOrFixture(e);
      return namespacesAndFixtures;
    }
  }
}