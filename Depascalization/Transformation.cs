using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Linq;
using Humanizer;

namespace Depascalization
{
  public class Transformation
  {
    public string Of(string input)
    {
      var parts = input.Split('(');
      return FirstPart(parts) + SecondPart(parts);
    }

    private static string SecondPart(string[] parts)
    {
      return (parts.Length > 1 ? " (" + parts[1] : string.Empty);
    }

    private static string FirstPart(string[] parts)
    {
      var humanizedVersion = parts[0].Humanize();
      if (humanizedVersion == string.Empty) return humanizedVersion;
      return Char.ToLowerInvariant(humanizedVersion[0]) + humanizedVersion.Substring(1);
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