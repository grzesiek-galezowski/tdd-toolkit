using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TddEbook.TddToolkit.Helpers.Constraints;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit
{
  public partial class XAssert
  {
    public static void TypeAdheresToConstraints<T>(List<IConstraint> constraints)
    {
      var violations = ConstraintsViolations.Empty();
      foreach (var constraint in constraints)
      {
        constraint.CheckAndRecord(violations);
      }

      violations.AssertNone();
    }

    public static void IsValueType<T>()
    {
      IsValueType<T>(ValueTypeTraits.Default());
    }

    public static void IsValueType<T>(ValueTypeTraits traits)
    {
      var activator = ValueObjectActivator<T>.FreshInstance();

      var constraints = CreateConstraintsBasedOn<T>(traits, activator);

      XAssert.TypeAdheresToConstraints<T>(constraints);
    }

    private static List<IConstraint> CreateConstraintsBasedOn<T>(ValueTypeTraits traits, ValueObjectActivator<T> activator)
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

      return constraints;
    }

    public static void IsValueType(Type type)
    {
      InvokeGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    private static void InvokeGenericVersionOfMethod(Type type, string name)
    {
      typeof(Any).GetMethods().Where(m => m.Name == name).First(m => m.GetParameters().Length == 0).
        MakeGenericMethod(new[] { type }).Invoke(null, null);
    }
  }
}
