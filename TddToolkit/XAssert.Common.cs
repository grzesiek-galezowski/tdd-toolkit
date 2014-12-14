using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NSubstitute;
using TddEbook.TddToolkit.Helpers.Constraints;
using TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator;
using TddEbook.TddToolkit.Helpers.Constraints.InequalityOperator;
using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit
{
  public partial class XAssert
  {
    public static void TypeAdheresToConstraints(IEnumerable<IConstraint> constraints)
    {
      var violations = ConstraintsViolations.Empty();
      foreach (var constraint in constraints)
      {
        RecordedAssertions.DoesNotThrow(() => constraint.CheckAndRecord(violations),
        "Did not expect exception", violations);
      }

      violations.AssertNone();
    }

    public static void IsValue<T>()
    {
      IsValue<T>(ValueTypeTraits.Default());
    }

    public static void IsValue<T>(ValueTypeTraits traits)
    {
      if (!typeof (T).IsPrimitive)
      {
        var activator = ValueObjectActivator.FreshInstance(typeof (T));

        var constraints = CreateConstraintsBasedOn(typeof (T), traits, activator);

        TypeAdheresToConstraints(constraints);
      }
    }

    private static IEnumerable<IConstraint> CreateConstraintsBasedOn(Type t, ValueTypeTraits traits, ValueObjectActivator activator)
    {

      var constraints = new List<IConstraint>();

      if(traits.RequireAllFieldsReadOnly)
      {
        constraints.Add(new AllFieldsMustBeReadOnly(t));
      }

      constraints.Add(new ThereMustBeNoPublicPropertySetters(t));
      constraints.Add(new StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualsMethod(activator));
      constraints.Add(new StateBasedEqualityMustBeImplementedInTermsOfEqualsMethod(activator));

      constraints.Add(new StateBasedUnEqualityMustBeImplementedInTermsOfEqualsMethod(activator, 
        traits.IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify.ToArray()));
      
      constraints.Add(new HashCodeMustBeTheSameForSameObjectsAndDifferentForDifferentObjects(activator,
        traits.IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify.ToArray()));

      if(traits.RequireSafeUnequalityToNull)
      {
        constraints.Add(new UnEqualityWithNullMustBeImplementedInTermsOfEqualsMethod(activator));
      }


      if (traits.RequireEqualityAndUnequalityOperatorImplementation)
      {
        //equality operator
        constraints.Add(new StateBasedEqualityShouldBeAvailableInTermsOfEqualityOperator(t));
        constraints.Add(new StateBasedEqualityMustBeImplementedInTermsOfEqualityOperator(activator));
        constraints.Add(new StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualityOperator(activator));
        constraints.Add(new StateBasedUnEqualityMustBeImplementedInTermsOfEqualityOperator(activator,
          traits.IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify.ToArray()));
        constraints.Add(new UnEqualityWithNullMustBeImplementedInTermsOfEqualityOperator(activator));

        //inequality operator
        constraints.Add(new StateBasedEqualityShouldBeAvailableInTermsOfInequalityOperator(t));
        constraints.Add(new StateBasedEqualityMustBeImplementedInTermsOfInequalityOperator(activator));
        constraints.Add(new StateBasedEqualityWithItselfMustBeImplementedInTermsOfInequalityOperator(activator));
        constraints.Add(new StateBasedUnEqualityMustBeImplementedInTermsOfInequalityOperator(activator,
          traits.IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify.ToArray()));
        constraints.Add(new UnEqualityWithNullMustBeImplementedInTermsOfInequalityOperator(activator));
      

      }
      return constraints;
    }

    public static void IsValue(Type type)
    {
      InvokeGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    private static void InvokeGenericVersionOfMethod(Type type, string name)
    {
      typeof(Any).GetMethods().Where(m => m.Name == name).First(m => m.GetParameters().Length == 0).
        MakeGenericMethod(new[] { type }).Invoke(null, null);
    }

    public static void IsEqualityOperatorDefinedFor<T>()
    {
      ExecutionOf(() => TypeOf<T>.Equality()).ShouldNotThrow<Exception>();
    }


    public static void IsInequalityOperatorDefinedFor<T>()
    {
      ExecutionOf(() => TypeOf<T>.Inequality()).ShouldNotThrow<Exception>();
    }

    public static void All(Action<AssertionRecorder> asserts)
    {
      var recorder = new AssertionRecorder();
      asserts.Invoke(recorder);

      recorder.AssertTrue();
    }

    private static Action ExecutionOf(Action func)
    {
      return func;
    }


    public static void IsEqualityOperatorDefinedFor(Type type)
    {
      ExecutionOf(() => TypeWrapper.For(type).Equality()).ShouldNotThrow<Exception>();
    }

    public static void IsInequalityOperatorDefinedFor(Type type)
    {
      ExecutionOf(() => TypeWrapper.For(type).Inequality()).ShouldNotThrow<Exception>();
    }
  }
}
