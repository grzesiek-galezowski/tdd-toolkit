using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit.Helpers.Constraints;
using TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator;
using TddEbook.TddToolkit.Helpers.Constraints.InequalityOperator;
using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;
using TddEbook.TypeReflection;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TddToolkit
{
  public partial class XAssert
  {
    public static void TypeAdheresTo(IEnumerable<IConstraint> constraints)
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

    public static void HasNullProtectedConstructors<T>()
    {
      var type = TypeWrapper.For(typeof(T));
      
      if (!type.HasConstructorWithParameters())
      {
        var constraints = new List<IConstraint> { new ConstructorsMustBeNullProtected(type.ToClrType())};
        TypeAdheresTo(constraints);
      }
    }

    public static void IsValue<T>(ValueTypeTraits traits)
    {
      if (!ValueObjectWhiteList.Contains<T>())
      {
        var activator = ValueObjectActivator.FreshInstance(typeof (T));
        var constraints = CreateConstraintsBasedOn(typeof (T), traits, activator);
        XAssert.TypeAdheresTo(constraints);
      }
    }


    private static IEnumerable<IConstraint> CreateConstraintsBasedOn(
      Type type, ValueTypeTraits traits, ValueObjectActivator activator)
    {

      var constraints = new List<IConstraint>();

      constraints.Add(new HasToBeAConcreteClass(type));

      if(traits.RequireAllFieldsReadOnly)
      {
        constraints.Add(new AllFieldsMustBeReadOnly(type));
      }

      constraints.Add(new ThereMustBeNoPublicPropertySetters(type));
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
        constraints.Add(new StateBasedEqualityShouldBeAvailableInTermsOfEqualityOperator(type));
        constraints.Add(new StateBasedEqualityMustBeImplementedInTermsOfEqualityOperator(activator));
        constraints.Add(new StateBasedEqualityWithItselfMustBeImplementedInTermsOfEqualityOperator(activator));
        constraints.Add(new StateBasedUnEqualityMustBeImplementedInTermsOfEqualityOperator(activator,
          traits.IndexesOfConstructorArgumentsIndexesThatDoNotContituteAValueIdentify.ToArray()));
        constraints.Add(new UnEqualityWithNullMustBeImplementedInTermsOfEqualityOperator(activator));

        //inequality operator
        constraints.Add(new StateBasedEqualityShouldBeAvailableInTermsOfInequalityOperator(type));
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
      var methodInfos = typeof(XAssert).GetMethods();
      var methodsByGivenName = methodInfos.Where(m => m.Name == name);
      var firstParameterlessVersion = methodsByGivenName.First(m => m.GetParameters().Length == 0);
      var genericMethod = firstParameterlessVersion.MakeGenericMethod(new[] { type });
      genericMethod.Invoke(null, null);
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

  class HasToBeAConcreteClass : IConstraint
  {
    private readonly Type _type;

    public HasToBeAConcreteClass(Type type)
    {
      _type = type;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      if (_type.IsAbstract)
      {
        violations.Add("Type " + _type + " is abstract but abstract classes cannot be value objects");
      }

      if (_type.IsInterface)
      {
        violations.Add("Type " + _type + " is an interface but interfaces cannot be value objects");
      }
    }
  }

  public class ConstructorsMustBeNullProtected : IConstraint
  {
    private readonly Type _type;

    public ConstructorsMustBeNullProtected(Type type)
    {
      _type = type;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var constructors = TypeWrapper.For(_type).GetAllPublicConstructors();
      var fallbackTypeGenerator = new FallbackTypeGenerator(_type);

      foreach (var constructor in constructors)
      {
        AssertNullCheckForEveryPossibleArgumentOf(violations, constructor, fallbackTypeGenerator);
      }      
    }

    private static void AssertNullCheckForEveryPossibleArgumentOf(IConstraintsViolations violations,
                                                                  IConstructorWrapper constructor,
                                                                  FallbackTypeGenerator fallbackTypeGenerator)
    {
      for (int i = 0; i < constructor.GetParametersCount(); ++i)
      {
        var parameters = constructor.GenerateAnyParameterValues(Any.Instance);
        if (TypeWrapper.ForTypeOf(parameters[i]).CanBeAssignedNullValue())
        {
          parameters[i] = null;

          try
          {
            fallbackTypeGenerator.GenerateInstance(parameters);
            violations.Add("Not guarded against nulls: " + constructor + ", Not guarded parameter: " +
                           constructor.GetDescriptionForParameter(i));
          }
          catch (TargetInvocationException exception)
          {
            if (exception.InnerException.GetType() == typeof (ArgumentNullException))
            {
              //do nothing, this is the expected case
            }
            else
            {
              throw;
            }
          }
        }
      }
    }
  }
}
