using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
      public void ShouldDePascalizeString(string input, string expected, string comment)
      {
        //GIVEN
        var depascalization = new Depascalization();

        //WHEN
        var depascalizedString = depascalization.Of(input);

        //THEN
        XAssert.Equal(expected, depascalizedString);
      }
    }
}
