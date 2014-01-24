namespace DePascalizationSpecification
{
  public static class ExampleNUnitReport
  {
    #region original report

    public const string Text = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""no""?>
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
                  <test-case name=""TddToolkitSpecification.AnySpecification.ShouldGenerateStringAccordingToRegex"" executed=""True"" result=""Success"" success=""True"" time=""0.238"" asserts=""1"" />
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

    #endregion

    public const string DepascalizedText = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""no""?>
<!--This file represents the results of running a test suite-->
<test-results name=""C:\Users\ftw637\Desktop\TddToolkitSpecification.nunit"" total=""43"" errors=""0"" failures=""0"" not-run=""0"" inconclusive=""0"" ignored=""0"" skipped=""0"" invalid=""0"" date=""2014-01-22"" time=""15:38:25"">
  <environment nunit-version=""2.6.2.12296"" clr-version=""2.0.50727.5472"" os-version=""Microsoft Windows NT 6.1.7601 Service Pack 1"" platform=""Win32NT"" cwd=""C:\Users\ftw637\Documents\GitHub\pickles\src\Pickles\packages\NUnit.Runners.2.6.2\tools"" machine-name=""FTW637-02"" user=""ftw637"" user-domain=""DS"" />
  <culture-info current-culture=""pl-PL"" current-uiculture=""en-US"" />
  <test-suite type=""Project"" name=""C:\Users\ftw637\Desktop\TddToolkitSpecification.nunit"" executed=""True"" result=""Success"" success=""True"" time=""6.297"" asserts=""0"">
    <results>
      <test-suite type=""Assembly"" name=""C:\Users\ftw637\Desktop\..\Documents\GitHub\tdd-toolkit\TddToolkitSpecification\bin\Debug\TddToolkitSpecification.dll"" executed=""True"" result=""Success"" success=""True"" time=""6.269"" asserts=""0"">
        <results>
          <test-suite type=""Namespace"" name=""TddToolkit Specification"" executed=""True"" result=""Success"" success=""True"" time=""6.269"" asserts=""0"">
            <results>
              <test-suite type=""TestFixture"" name=""Any Specification"" executed=""True"" result=""Success"" success=""True"" time=""5.182"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkit Specification. Any Specification. should allow creating custom collection instances"" executed=""True"" result=""Success"" success=""True"" time=""3.414"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should always return the different value from proxied the same method on different object"" executed=""True"" result=""Success"" success=""True"" time=""0.136"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should always return the same value from proxied method on the same object"" executed=""True"" result=""Success"" success=""True"" time=""0.012"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should be able to generate both primitive type instance and interface using new instance method"" executed=""True"" result=""Success"" success=""True"" time=""0.043"" asserts=""1"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should be able to generate distinct digits each time"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""3"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should be able to generate distinct letters each time"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""3"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should be able to generate instances of abstract classes"" executed=""True"" result=""Success"" success=""True"" time=""0.141"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should be able to generate instances of concrete classes with interfaces as their constructor arguments"" executed=""True"" result=""Success"" success=""True"" time=""0.041"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should be able to proxy concrete return types of methods"" executed=""True"" result=""Success"" success=""True"" time=""0.013"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should be able to proxy methods that return interfaces"" executed=""True"" result=""Success"" success=""True"" time=""0.023"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should create different exception each time"" executed=""True"" result=""Success"" success=""True"" time=""0.134"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should create non null uri"" executed=""True"" result=""Success"" success=""True"" time=""0.015"" asserts=""1"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should create sorted set with three distinct values"" executed=""True"" result=""Success"" success=""True"" time=""0.057"" asserts=""2"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should generate different integer each time"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should generate different type each time"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should generate different value each time and not among passed ones when asked to create any value besides given"" executed=""True"" result=""Success"" success=""True"" time=""0.007"" asserts=""2"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should generate finite enumerables"" executed=""True"" result=""Success"" success=""True"" time=""0.041"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should generate members returning type of type"" executed=""True"" result=""Success"" success=""True"" time=""0.022"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should generate pick next value each time from passed ones when asked to create any value from given"" executed=""True"" result=""Success"" success=""True"" time=""0.021"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should generate string according to regex"" executed=""True"" result=""Success"" success=""True"" time=""0.238"" asserts=""1"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should generate string of given length"" executed=""True"" result=""Success"" success=""True"" time=""0.003"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should handle empty excluded strings when generating any string not containing given"" executed=""True"" result=""Success"" success=""True"" time=""0.012"" asserts=""1"">
                    <properties>
                      <property name=""Timeout"" value=""1000"" />
                    </properties>
                  </test-case>
                  <test-case name=""TddToolkit Specification. Any Specification. should not create the same method info twice in a row"" executed=""True"" result=""Success"" success=""True"" time=""0.003"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should override virtual methods that return default type values on abstract class proxy"" executed=""True"" result=""Success"" success=""True"" time=""0.015"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should override virtual methods that throw exceptions on abstract class proxy"" executed=""True"" result=""Success"" success=""True"" time=""0.009"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should support actions"" executed=""True"" result=""Success"" success=""True"" time=""0.126"" asserts=""1"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should support creation of key value pairs"" executed=""True"" result=""Success"" success=""True"" time=""0.026"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should support generating ranged collections"" executed=""True"" result=""Success"" success=""True"" time=""0.312"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should support recursive interfaces with dictionaries"" executed=""True"" result=""Success"" success=""True"" time=""0.058"" asserts=""2"" />
                  <test-case name=""TddToolkit Specification. Any Specification. should support recursive interfaces with lists"" executed=""True"" result=""Success"" success=""True"" time=""0.095"" asserts=""1"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""CircularList Specification"" executed=""True"" result=""Success"" success=""True"" time=""0.007"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkit Specification. CircularList Specification. should return all elements in order they were added"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. CircularList Specification. should start over returning elements when it runs out of elements"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""0"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""ConstraintsViolations Specification"" executed=""True"" result=""Success"" success=""True"" time=""0.330"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkit Specification. ConstraintsViolations Specification. should not throw exception when no violations have been added"" executed=""True"" result=""Success"" success=""True"" time=""0.003"" asserts=""1"" />
                  <test-case name=""TddToolkit Specification. ConstraintsViolations Specification. should throw exception containing all violation messages when more than one violation was added"" executed=""True"" result=""Success"" success=""True"" time=""0.318"" asserts=""4"" />
                  <test-case name=""TddToolkit Specification. ConstraintsViolations Specification. should throw exception when at least one violation was added"" executed=""True"" result=""Success"" success=""True"" time=""0.001"" asserts=""1"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""RecordedAssertions Specification"" executed=""True"" result=""Success"" success=""True"" time=""0.546"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkit Specification. RecordedAssertions Specification. should add error message when truth assertion fails"" executed=""True"" result=""Success"" success=""True"" time=""0.534"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. RecordedAssertions Specification. should not add error message when truth assertion passes"" executed=""True"" result=""Success"" success=""True"" time=""0.007"" asserts=""0"" />
                </results>
              </test-suite>
              <test-suite type=""TestFixture"" name=""XAssert Specification"" executed=""True"" result=""Success"" success=""True"" time=""0.184"" asserts=""0"">
                <results>
                  <test-case name=""TddToolkit Specification. XAssert Specification. should accept proper full value types and reject bad ones"" executed=""True"" result=""Success"" success=""True"" time=""0.072"" asserts=""1"" />
                  <test-case name=""TddToolkit Specification. XAssert Specification. should aggregate multiple assertions when assertion all"" executed=""True"" result=""Success"" success=""True"" time=""0.041"" asserts=""6"" />
                  <test-case name=""TddToolkit Specification. XAssert Specification. should allow specifying constructor arguments not taken into account during value behavior check"" executed=""True"" result=""Success"" success=""True"" time=""0.017"" asserts=""1"" />
                  <test-case name=""TddToolkit Specification. XAssert Specification. should not throw exception when attribute is on method"" executed=""True"" result=""Success"" success=""True"" time=""0.019"" asserts=""1"" />
                  <test-case name=""TddToolkit Specification. XAssert Specification. should pass value type assertion for proper value type"" executed=""True"" result=""Success"" success=""True"" time=""0.006"" asserts=""0"" />
                  <test-case name=""TddToolkit Specification. XAssert Specification. should throw exception when attribute is not on method"" executed=""True"" result=""Success"" success=""True"" time=""0.002"" asserts=""1"" />
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