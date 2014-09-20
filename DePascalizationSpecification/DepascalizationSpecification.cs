using System.Globalization;
using NUnit.Framework;
using System;
using TddEbook.TddToolkit;

namespace DePascalizationSpecification
{
    public class DepascalizationSpecification
    {
      [TestCase(      "",       "", "Empty string")]
      [TestCase(     "A",      "a", "Single letter")]
      [TestCase("Should", "should", "Single word")]
      [TestCase("ShouldDo", "should do", "Many words")]
      [TestCase("ShouldParseXML", "should parse XML", "consecutive big letters")]
      [TestCase("ShouldConvert0Into1", "should convert 0 into 1", "single digits")]
      [TestCase("ShouldAdd12And13ToGet25", "should add 12 and 13 to get 25", "more digits")]
      [TestCase("ShouldMakeAPostWhenIGetIt", "should make a post when I get it", "single letter word in the middle")]
      [TestCase("ShouldSpySOAPRequest", "should spy SOAP request", "Abbreviation followed by word")]
      [TestCase("ShouldConvertFrameToPRRP12Format", "should convert frame to PRRP 12 format", "")]
      [TestCase("ShouldDoWhatever(\"SomeString\", 23, Users.AdminUser)", "should do whatever (\"SomeString\", 23, Users.AdminUser)", "Parameterized")]
      public void ShouldDepascalizeString(string input, string expected, string comment)
      {
        //GIVEN
        var depascalization = new Depascalization.Depascalization();

        //WHEN
        var depascalizedString = depascalization.Of(input);

        //THEN
        XAssert.Equal(expected, depascalizedString);
      }

      [Test]
      public void ShouldAllowDepascalizationOfNUnitReportXml()
      {
        //GIVEN
        const string input = ExampleNUnitReport.Text;
        const string expectedReport = ExampleNUnitReport.DepascalizedText;
        var splitExpected = expectedReport.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        var depascalization = new Depascalization.Depascalization();

        //WHEN
        var depascalizedVersion = depascalization.OfNUnitReport(input);

        //THEN
        var splitResult = depascalizedVersion.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        XAssert.All(assert =>
          {
            for (var i = 0; i < splitExpected.Length ; i++)
            {
              assert.Equal(splitExpected[i].Trim(), splitResult[i].Trim(), i.ToString(CultureInfo.InvariantCulture));
            }
          });
      }
    }
}
