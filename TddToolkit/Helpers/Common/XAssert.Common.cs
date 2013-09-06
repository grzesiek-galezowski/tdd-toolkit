using System;
using System.Collections.Generic;
using KellermanSoftware.CompareNetObjects;
using TddEbook.TddToolkit.Helpers.Constraints;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Common
{
  public partial class XAssert
  {
    public static void AreAlike<T>(T expected, T actual)
    {
      var comparison = ObjectLikenessComparison();
      if (!comparison.Compare(expected, actual))
      {
        throw new NotAlikeException(comparison.DifferencesString);
      }
    }

    public static void AreNotAlike<T>(T expected, T actual)
    {
      var comparison = ObjectLikenessComparison();
      if (comparison.Compare(expected, actual))
      {
        throw new AlikeException(comparison.DifferencesString);
      }
    }

    public static void TypeAdheresToConstraints<T>(List<IConstraint<T>> constraints)
    {
      var violations = ConstraintsViolations.Empty();
      foreach (var constraint in constraints)
      {
        constraint.CheckAndRecord(violations);
      }

      violations.AssertNone();
    }

    private static CompareObjects ObjectLikenessComparison()
    {
      var comparisonMechanism = new CompareObjects()
      {
        CompareChildren = true,
        CompareFields = true,
        ComparePrivateFields = true,
        ComparePrivateProperties = true,
        CompareProperties = true,
        CompareReadOnly = true,
        MaxDifferences = 1
      };
      AddCriteriaForComparingTypesReferenceTo(comparisonMechanism);
      return comparisonMechanism;
    }

    private static void AddCriteriaForComparingTypesReferenceTo(CompareObjects compareObjects)
    {
      compareObjects.IsUseCustomTypeComparer = type => IsPartOfReflectionApi(type) || IsDynamicProxy(type);

      compareObjects.CustomComparer = (objects, o1, o2, arg4) =>
        {
          if (!object.ReferenceEquals(o1, o2))
          {
            objects.Differences.Add(new Difference()
              {
                Object1Value = o1.ToString(),
                Object2Value = o2.ToString(),
                ActualName = "Reference to " + o2.GetType() + ": ",
                ExpectedName = "Reference to " + o1.GetType(),
              });
          }
        };
    }

    private static bool IsPartOfReflectionApi(Type type)
    {
      return type.Namespace != null && type.Namespace.StartsWith("System.Reflection");
    }

    private static bool IsDynamicProxy(Type type)
    {
      return type.Namespace != null && type.Namespace.StartsWith("Castle.");
    }


    public static void IsValueType<T>()
    {

      var violations = new ConstraintsViolations();
      var activator = ValueObjectActivator<T>.FreshInstance();
      
      var constraints = new List<IConstraint<T>>()
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


  }
}
