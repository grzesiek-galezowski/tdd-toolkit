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
      var violations = new ConstraintsViolations();
      var activator = ValueObjectActivator<T>.FreshInstance();
      
      var constraints = new List<IConstraint>
      {
        new AllFieldsMustBeReadOnly<T>(),
        new ThereMustBeNoPublicPropertySetters<T>(),
        new StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualsMethod<T>(activator),
        new StateBasedEqualityMustBeImplementedInTermsOfEqualsMethod<T>(activator),
        new StateBasedUnEqualityMustBeImplementedInTermsOfEqualsMethod<T>(activator),
        new UnEqualityWithNullMustBeImplementedInTermsOfEqualsMethod<T>(activator)
      };

      foreach (var constraint in constraints)
      {
        constraint.CheckAndRecord(violations);
      }

      violations.AssertNone();
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
