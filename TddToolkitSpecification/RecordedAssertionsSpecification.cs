using System;
using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;

namespace TddEbook.TddToolkitSpecification
{
  class RecordedAssertionsSpecification
  {
    [Test]
    public void ShouldAddErrorMessageWhenTruthAssertionFails()
    {
      //GIVEN
      var violations = Substitute.For<IConstraintsViolations>();
      var anyMessage = Any.String();

      //WHEN
      RecordedAssertions.True(false, anyMessage, violations);

      //THEN
      violations.Received(1).Add(anyMessage);
    }

    [Test]
    public void ShouldNotAddErrorMessageWhenTruthAssertionPasses()
    {
      //GIVEN
      var violations = Substitute.For<IConstraintsViolations>();
      var anyMessage = Any.String();

      //WHEN
      RecordedAssertions.True(true, anyMessage, violations);

      //THEN
      violations.DidNotReceive().Add(anyMessage);
    }


    [Test]
    public void ShouldFailStaticFieldsAssertionIfAssemblyContainsAtLeastOneStaticField()
    {
      var assembly = typeof (RecordedAssertionsSpecification).Assembly;
      
      var e = Assert.Throws<AssertionException>(() => XAssert.NoStaticFields(assembly));
      StringAssert.Contains("_lolek", e.Message);
      StringAssert.Contains("_gieniek", e.Message);
      StringAssert.Contains("StaticProperty", e.Message);
    }

    [Test]
    public void ShouldFailReferenceAssertionWhenAssemblyReferencesOtherAssembly()
    {
      var assembly1 = typeof(RecordedAssertionsSpecification).Assembly;
      Assert.Throws<AssertionException>(() => XAssert.NoReference(assembly1, typeof(TestAttribute)));
    }


    [Test] //TODO 1. only single constructor! 2. class inheritance levels
    public void ShouldFailNonPublicEventsAssertionWhenAssemblyContainsAtLeastOneNonPublicEvent()
    {
      var assembly = typeof (RecordedAssertionsSpecification).Assembly;
      var ex = Assert.Throws<AssertionException>(() => XAssert.NoNonPublicEvents(assembly));
      StringAssert.DoesNotContain("explicitlyImplementedEvent", ex.Message);
    }

    [Test] //TODO 1. only single constructor! 2. class inheritance levels, 3. private/protected events
    public void ShouldFailConstructorLimitAssertionWhenAnyClassContainsAtLeastOneConstructor()
    {
      var assembly = typeof (RecordedAssertionsSpecification).Assembly;
      var exception = Assert.Throws<AssertionException>(() => XAssert.SingleConstructor(assembly));
      StringAssert.DoesNotContain("MyException", exception.Message);
    }



    public class Lol2
    {
#pragma warning disable 169
      private static int _gieniek = 123;
#pragma warning restore 169
    }

#pragma warning disable 67
    protected event AnyEventHandler _changed;
#pragma warning restore 67


#pragma warning disable 169
    private static int _lolek = 12;
#pragma warning restore 169

    public static int StaticProperty
    {
      get;
      set;
    }

  }

  public delegate void AnyEventHandler(object sender, AnyEventHandlerArgs args);

  public interface AnyEventHandlerArgs
  {
  }

  public class ObjectWithTwoConstructors
  {
    public ObjectWithTwoConstructors(int x)
    {
      
    }

    public ObjectWithTwoConstructors(string x)
    {

    }
  }

  public class MyException : Exception
  {
    
  }

  public interface ExplicitlyImplemented
  {
    event AnyEventHandler explicitlyImplementedEvent;    
  }

  public class ExplicitImplementation : ExplicitlyImplemented
  {
    event AnyEventHandler ExplicitlyImplemented.explicitlyImplementedEvent
    {
      add { throw new NotImplementedException(); }
      remove { throw new NotImplementedException(); }
    }
  }
}
