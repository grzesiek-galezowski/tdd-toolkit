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
        var input = ExampleNUnitReport.Text;
        
        var expectedReport = ExampleNUnitReport.DepascalizedText;
        var splitExpected = expectedReport.Split('\n');
        var depascalization = new Depascalization.Depascalization();

        //WHEN
        var depascalizedVersion = depascalization.OfNUnitReport(input);

        //THEN
        var splitResult = depascalizedVersion.Split('\n');
        for (int i = 0; i < splitExpected.Length ; i++)
        {
          XAssert.Equal(splitExpected[i], splitResult[i], i.ToString());
        }
      }

    }


  public class ExampleNUnitReport
  {
    public static string Text = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""no""?>
<!--This file represents the results of running a test suite-->
<test-results name=""C:\Users\ftw637\Desktop\TddToolkitSpecification.nunit"" total=""43"" errors=""0"" failures=""0"" not-run=""0"" inconclusive=""0"" ignored=""0"" skipped=""0"" invalid=""0"" date=""2014-01-22"" time=""15:38:25"">
  <environment nunit-version=""2.6.2.12296"" clr-version=""2.0.50727.5472"" os-version=""Microsoft Windows NT 6.1.7601 Service Pack 1"" platform=""Win32NT"" cwd=""C:\Users\ftw637\Documents\GitHub\pickles\src\Pickles\packages\NUnit.Runners.2.6.2\tools"" machine-name=""FTW637-02"" user=""ftw637"" user-domain=""DS"" />
  <culture-info current-culture=""pl-PL"" current-uiculture=""en-US"" />
  <test-suite type=""Project"" name=""C:\Users\ftw637\Desktop\TddToolkitSpecification.nunit"" executed=""True"" result=""Success"" success=""True"" time=""6.297"" asserts=""0"">
    <results>
      <test-suite type=""Assembly"" name=""C:\Users\ftw637\Desktop\..\Documents\GitHub\tdd-toolkit\TddToolkitSpecification\bin\Debug\TddToolkitSpecification.dll"" executed=""True"" result=""Success"" success=""True"" time=""6.269"" asserts=""0"">
        <results>
          <test-suite type=""Namespace"" name=""TddToolkitSpecification"" executed=""True"" result=""Success"" success=""True"" time=""6.269"" asserts=""0"">
            <results>
              <test-suite type=""TestFixture"" name=""AnySpecification"" executed=""True"" result=""Success"" success=""True"" time=""5.182"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldAllowCreatingCustomCollectionInstances"" executed=""True"" result=""Success"" success=""True"" time=""3.414"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldAlwaysReturnTheDifferentValueFromProxiedTheSameMethodOnDifferentObject"" executed=""True"" result=""Success"" success=""True"" time=""0.136"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldAlwaysReturnTheSameValueFromProxiedMethodOnTheSameObject"" executed=""True"" result=""Success"" success=""True"" time=""0.012"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldBeAbleToGenerateBothPrimitiveTypeInstanceAndInterfaceUsingNewInstanceMethod"" executed=""True"" result=""Success"" success=""True"" time=""0.043"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldBeAbleToGenerateDistinctDigitsEachTime"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""3"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldBeAbleToGenerateDistinctLettersEachTime"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""3"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldBeAbleToGenerateInstancesOfAbstractClasses"" executed=""True"" result=""Success"" success=""True"" time=""0.141"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldBeAbleToGenerateInstancesOfConcreteClassesWithInterfacesAsTheirConstructorArguments"" executed=""True"" result=""Success"" success=""True"" time=""0.041"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldBeAbleToProxyConcreteReturnTypesOfMethods"" executed=""True"" result=""Success"" success=""True"" time=""0.013"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldBeAbleToProxyMethodsThatReturnInterfaces"" executed=""True"" result=""Success"" success=""True"" time=""0.023"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldCreateDifferentExceptionEachTime"" executed=""True"" result=""Success"" success=""True"" time=""0.134"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldCreateNonNullUri"" executed=""True"" result=""Success"" success=""True"" time=""0.015"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldCreateSortedSetWithThreeDistinctValues"" executed=""True"" result=""Success"" success=""True"" time=""0.057"" asserts=""2"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldGenerateDifferentIntegerEachTime"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldGenerateDifferentTypeEachTime"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldGenerateDifferentValueEachTimeAndNotAmongPassedOnesWhenAskedToCreateAnyValueBesidesGiven"" executed=""True"" result=""Success"" success=""True"" time=""0.007"" asserts=""2"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldGenerateFiniteEnumerables"" executed=""True"" result=""Success"" success=""True"" time=""0.041"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldGenerateMembersReturningTypeOfType"" executed=""True"" result=""Success"" success=""True"" time=""0.022"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldGeneratePickNextValueEachTimeFromPassedOnesWhenAskedToCreateAnyValueFromGiven"" executed=""True"" result=""Success"" success=""True"" time=""0.021"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldGenerateStringAccordingtoRegex"" executed=""True"" result=""Success"" success=""True"" time=""0.238"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldGenerateStringOfGivenLength"" executed=""True"" result=""Success"" success=""True"" time=""0.003"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldHandleEmptyExcludedStringsWhenGeneratingAnyStringNotContainingGiven"" executed=""True"" result=""Success"" success=""True"" time=""0.012"" asserts=""1"">
                    <properties>
                      <property name=""Timeout"" value=""1000"" />
                    </properties>
                  </test-case>
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldNotCreateTheSameMethodInfoTwiceInARow"" executed=""True"" result=""Success"" success=""True"" time=""0.003"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldOverrideVirtualMethodsThatReturnDefaultTypeValuesOnAbstractClassProxy"" executed=""True"" result=""Success"" success=""True"" time=""0.015"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldOverrideVirtualMethodsThatThrowExceptionsOnAbstractClassProxy"" executed=""True"" result=""Success"" success=""True"" time=""0.009"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldSupportActions"" executed=""True"" result=""Success"" success=""True"" time=""0.126"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldSupportCreationOfKeyValuePairs"" executed=""True"" result=""Success"" success=""True"" time=""0.026"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldSupportGeneratingRangedCollections"" executed=""True"" result=""Success"" success=""True"" time=""0.312"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldSupportRecursiveInterfacesWithDictionaries"" executed=""True"" result=""Success"" success=""True"" time=""0.058"" asserts=""2"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldSupportRecursiveInterfacesWithLists"" executed=""True"" result=""Success"" success=""True"" time=""0.095"" asserts=""1"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""CircularListSpecification"" executed=""True"" result=""Success"" success=""True"" time=""0.007"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkitSpecification.CircularListSpecification.ShouldReturnAllElementsInOrderTheyWereAdded"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.CircularListSpecification.ShouldStartOverReturningElementsWhenItRunsOutOfElements"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""0"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""ConstraintsViolationsSpecification"" executed=""True"" result=""Success"" success=""True"" time=""0.330"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkitSpecification.ConstraintsViolationsSpecification.ShouldNotThrowExceptionWhenNoViolationsHaveBeenAdded"" executed=""True"" result=""Success"" success=""True"" time=""0.003"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.ConstraintsViolationsSpecification.ShouldThrowExceptionContainingAllViolationMessagesWhenMoreThanOneViolationWasAdded"" executed=""True"" result=""Success"" success=""True"" time=""0.318"" asserts=""4"" />
                  <test-case name=""TddToolkitSpecification.ConstraintsViolationsSpecification.ShouldThrowExceptionWhenAtLeastOneViolationWasAdded"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""1"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""RecordedAssertionsSpecification"" executed=""True"" result=""Success"" success=""True"" time=""0.546"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkitSpecification.RecordedAssertionsSpecification.ShouldAddErrorMessageWhenTruthAssertionFails"" executed=""True"" result=""Success"" success=""True"" time=""0.534"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.RecordedAssertionsSpecification.ShouldNotAddErrorMessageWhenTruthAssertionPasses"" executed=""True"" result=""Success"" success=""True"" time=""0.007"" asserts=""0"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""XAssertSpecification"" executed=""True"" result=""Success"" success=""True"" time=""0.184"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldAcceptProperFullValueTypesAndRejectBadOnes"" executed=""True"" result=""Success"" success=""True"" time=""0.072"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldAggregateMultipleAssertionsWhenAssertionAll"" executed=""True"" result=""Success"" success=""True"" time=""0.041"" asserts=""6"" />
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldAllowSpecifyingConstructorArgumentsNotTakenIntoAccountDuringValueBehaviorCheck"" executed=""True"" result=""Success"" success=""True"" time=""0.017"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldNotThrowExceptionWhenAttributeIsOnMethod"" executed=""True"" result=""Success"" success=""True"" time=""0.019"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldPassValueTypeAssertionForProperValueType"" executed=""True"" result=""Success"" success=""True"" time=""0.006"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldThrowExceptionWhenAttributeIsNotOnMethod"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""1"" />
                </results>
              </test-suite>
            </results>
          </test-suite>
        </results>
      </test-suite>
    </results>
  </test-suite>
</test-results>";
    public static string DepascalizedText = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""no""?>
<!--This file represents the results of running a test suite-->
<test-results name=""C:\Users\ftw637\Desktop\TddToolkitSpecification.nunit"" total=""43"" errors=""0"" failures=""0"" not-run=""0"" inconclusive=""0"" ignored=""0"" skipped=""0"" invalid=""0"" date=""2014-01-22"" time=""15:38:25"">
  <environment nunit-version=""2.6.2.12296"" clr-version=""2.0.50727.5472"" os-version=""Microsoft Windows NT 6.1.7601 Service Pack 1"" platform=""Win32NT"" cwd=""C:\Users\ftw637\Documents\GitHub\pickles\src\Pickles\packages\NUnit.Runners.2.6.2\tools"" machine-name=""FTW637-02"" user=""ftw637"" user-domain=""DS"" />
  <culture-info current-culture=""pl-PL"" current-uiculture=""en-US"" />
  <test-suite type=""Project"" name=""C:\Users\ftw637\Desktop\TddToolkitSpecification.nunit"" executed=""True"" result=""Success"" success=""True"" time=""6.297"" asserts=""0"">
    <results>
      <test-suite type=""Assembly"" name=""C:\Users\ftw637\Desktop\..\Documents\GitHub\tdd-toolkit\TddToolkitSpecification\bin\Debug\TddToolkitSpecification.dll"" executed=""True"" result=""Success"" success=""True"" time=""6.269"" asserts=""0"">
        <results>
          <test-suite type=""Namespace"" name=""TddToolkitSpecification"" executed=""True"" result=""Success"" success=""True"" time=""6.269"" asserts=""0"">
            <results>
              <test-suite type=""TestFixture"" name=""Any Specification"" executed=""True"" result=""Success"" success=""True"" time=""5.182"" asserts=""0"">
                <results>
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should allow creating custom collection instances"" executed=""True"" result=""Success"" success=""True"" time=""3.414"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should always return the different value from proxied the same method on different object"" executed=""True"" result=""Success"" success=""True"" time=""0.136"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should always Return The Same Value From Proxied Method On The Same Object"" executed=""True"" result=""Success"" success=""True"" time=""0.012"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should be able To Generate Both Primitive Type Instance And Interface Using New Instance Method"" executed=""True"" result=""Success"" success=""True"" time=""0.043"" asserts=""1"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should be able To Generate Distinct Digits Each Time"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""3"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should be able To Generate Distinct Letters Each Time"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""3"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should be able To Generate Instances Of Abstract Classes"" executed=""True"" result=""Success"" success=""True"" time=""0.141"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should be able To Generate Instances Of Concrete Classes With Interfaces As Their Constructor Arguments"" executed=""True"" result=""Success"" success=""True"" time=""0.041"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should be able To Proxy Concrete Return Types Of Methods"" executed=""True"" result=""Success"" success=""True"" time=""0.013"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should be able To Proxy Methods That Return Interfaces"" executed=""True"" result=""Success"" success=""True"" time=""0.023"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should create different Exception Each Time"" executed=""True"" result=""Success"" success=""True"" time=""0.134"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should create non Null Uri"" executed=""True"" result=""Success"" success=""True"" time=""0.015"" asserts=""1"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should create sorted Set With Three Distinct Values"" executed=""True"" result=""Success"" success=""True"" time=""0.057"" asserts=""2"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should generate different Integer Each Time"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should generate different Type Each Time"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should generate different Value Each Time And Not Among Passed Ones When Asked To Create Any Value Besides Given"" executed=""True"" result=""Success"" success=""True"" time=""0.007"" asserts=""2"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should generate finite Enumerables"" executed=""True"" result=""Success"" success=""True"" time=""0.041"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should generate members Returning Type Of Type"" executed=""True"" result=""Success"" success=""True"" time=""0.022"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should generate pick Next Value Each Time From Passed Ones When Asked To Create Any Value From Given"" executed=""True"" result=""Success"" success=""True"" time=""0.021"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should generate string According to Regex"" executed=""True"" result=""Success"" success=""True"" time=""0.238"" asserts=""1"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should generate string Of Given Length"" executed=""True"" result=""Success"" success=""True"" time=""0.003"" asserts=""0"" />
                  <test-case name=""Tdd Toolkit Specification / Any Specification / should gandle empty Excluded Strings When Generating Any String Not Containing Given"" executed=""True"" result=""Success"" success=""True"" time=""0.012"" asserts=""1"">
                    <properties>
                      <property name=""Timeout"" value=""1000"" />
                    </properties>
                  </test-case>
                  <test-case name=""TddToolkitSpecification.AnySpecification.Should Not CreateTheSameMethodInfoTwiceInARow"" executed=""True"" result=""Success"" success=""True"" time=""0.003"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.Should Override Virtual Methods That Return Default Type Values On Abstract Class Proxy"" executed=""True"" result=""Success"" success=""True"" time=""0.015"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.Should Override Virtual Methods That Throw Exceptions On Abstract Class Proxy"" executed=""True"" result=""Success"" success=""True"" time=""0.009"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.Should Support Actions"" executed=""True"" result=""Success"" success=""True"" time=""0.126"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.Should Support Creation Of Key Value Pairs"" executed=""True"" result=""Success"" success=""True"" time=""0.026"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.Should Support Generating Ranged Collections"" executed=""True"" result=""Success"" success=""True"" time=""0.312"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.Should Support Recursive Interfaces With Dictionaries"" executed=""True"" result=""Success"" success=""True"" time=""0.058"" asserts=""2"" />
                  <test-case name=""TddToolkitSpecification.AnySpecification.Should Support Recursive Interfaces With Lists"" executed=""True"" result=""Success"" success=""True"" time=""0.095"" asserts=""1"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""CircularListSpecification"" executed=""True"" result=""Success"" success=""True"" time=""0.007"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkitSpecification.CircularListSpecification.ShouldReturnAllElementsInOrderTheyWereAdded"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.CircularListSpecification.ShouldStartOverReturningElementsWhenItRunsOutOfElements"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""0"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""ConstraintsViolationsSpecification"" executed=""True"" result=""Success"" success=""True"" time=""0.330"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkitSpecification.ConstraintsViolationsSpecification.ShouldNotThrowExceptionWhenNoViolationsHaveBeenAdded"" executed=""True"" result=""Success"" success=""True"" time=""0.003"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.ConstraintsViolationsSpecification.ShouldThrowExceptionContainingAllViolationMessagesWhenMoreThanOneViolationWasAdded"" executed=""True"" result=""Success"" success=""True"" time=""0.318"" asserts=""4"" />
                  <test-case name=""TddToolkitSpecification.ConstraintsViolationsSpecification.ShouldThrowExceptionWhenAtLeastOneViolationWasAdded"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""1"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""RecordedAssertionsSpecification"" executed=""True"" result=""Success"" success=""True"" time=""0.546"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkitSpecification.RecordedAssertionsSpecification.ShouldAddErrorMessageWhenTruthAssertionFails"" executed=""True"" result=""Success"" success=""True"" time=""0.534"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.RecordedAssertionsSpecification.ShouldNotAddErrorMessageWhenTruthAssertionPasses"" executed=""True"" result=""Success"" success=""True"" time=""0.007"" asserts=""0"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""XAssertSpecification"" executed=""True"" result=""Success"" success=""True"" time=""0.184"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldAcceptProperFullValueTypesAndRejectBadOnes"" executed=""True"" result=""Success"" success=""True"" time=""0.072"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldAggregateMultipleAssertionsWhenAssertionAll"" executed=""True"" result=""Success"" success=""True"" time=""0.041"" asserts=""6"" />
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldAllowSpecifyingConstructorArgumentsNotTakenIntoAccountDuringValueBehaviorCheck"" executed=""True"" result=""Success"" success=""True"" time=""0.017"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldNotThrowExceptionWhenAttributeIsOnMethod"" executed=""True"" result=""Success"" success=""True"" time=""0.019"" asserts=""1"" />
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldPassValueTypeAssertionForProperValueType"" executed=""True"" result=""Success"" success=""True"" time=""0.006"" asserts=""0"" />
                  <test-case name=""TddToolkitSpecification.XAssertSpecification.ShouldThrowExceptionWhenAttributeIsNotOnMethod"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""1"" />
                </results>
              </test-suite>
            </results>
          </test-suite>
        </results>
      </test-suite>
    </results>
  </test-suite>
</test-results>";
  }
}
