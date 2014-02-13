using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using TddEbook.TddToolkit.Helpers.Constraints;
using TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit
{
  public partial class XAssert
  {
    public static void TypeAdheresToConstraints<T>(IEnumerable<IConstraint> constraints) where T : class
    {
      var violations = ConstraintsViolations.Empty();
      foreach (var constraint in constraints)
      {
        constraint.CheckAndRecord(violations);
      }

      violations.AssertNone();
    }

    public static void IsValue<T>() where T : class
    {
      IsValue<T>(ValueTypeTraits.Default());
    }

    public static void IsValue<T>(ValueTypeTraits traits) where T : class
    {
      var activator = ValueObjectActivator<T>.FreshInstance();

      var constraints = CreateConstraintsBasedOn<T>(traits, activator);

      XAssert.TypeAdheresToConstraints<T>(constraints);
    }

    private static IEnumerable<IConstraint> CreateConstraintsBasedOn<T>
      (ValueTypeTraits traits, ValueObjectActivator<T> activator)
      where T : class
    {
      var constraints = new List<IConstraint>();

      if(traits.RequireAllFieldsReadOnly)
      {
        constraints.Add(new AllFieldsMustBeReadOnly<T>());
      }

      constraints.Add(new ThereMustBeNoPublicPropertySetters<T>());
      constraints.Add(new StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualsMethod<T>(activator));
      constraints.Add(new StateBasedEqualityMustBeImplementedInTermsOfEqualsMethod<T>(activator));

      constraints.Add(new StateBasedUnEqualityMustBeImplementedInTermsOfEqualsMethod<T>(activator, 
        traits.IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify.ToArray()));
      
      constraints.Add(new HashCodeMustBeTheSameForSameObjectsAndDifferentForDifferentObjects<T>(activator,
        traits.IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify.ToArray()));

      if(traits.RequireSafeUnequalityToNull)
      {
        constraints.Add(new UnEqualityWithNullMustBeImplementedInTermsOfEqualsMethod<T>(activator));
      }


      if (traits.RequireEqualityAndUnequalityOperatorImplementation)
      {
        //equality operator
        constraints.Add(new StateBasedEqualityShouldBeAvailableInTermsOfEqualityOperator<T>());
        constraints.Add(new StateBasedEqualityMustBeImplementedInTermsOfEqualityOperator<T>(activator));
        constraints.Add(new StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualityOperator<T>(activator));
        constraints.Add(new StateBasedUnEqualityMustBeImplementedInTermsOfEqualityOperator<T>(activator,
          traits.IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify.ToArray()));
        constraints.Add(new UnEqualityWithNullMustBeImplementedInTermsOfEqualityOperator<T>(activator));

        //inequality operator
        constraints.Add(new StateBasedEqualityShouldBeAvailableInTermsOfInequalityOperator<T>());
        constraints.Add(new StateBasedEqualityMustBeImplementedInTermsOfInequalityOperator<T>(activator));
        constraints.Add(new StateBasedEqualityWithItselfMustBeImplementedInTermsOfInequalityOperator<T>(activator));
        constraints.Add(new StateBasedUnEqualityMustBeImplementedInTermsOfInequalityOperator<T>(activator,
          traits.IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify.ToArray()));
        constraints.Add(new UnEqualityWithNullMustBeImplementedInTermsOfInequalityOperator<T>(activator));
      

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
      ExecutionOf(() => BinaryOperator.EqualityFrom<T>()).ShouldNotThrow<Exception>();
    }


    public static void IsInequalityOperatorDefinedFor<T>()
    {
      ExecutionOf(() => BinaryOperator.InequalityFrom<T>()).ShouldNotThrow<Exception>();
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

  }
}
