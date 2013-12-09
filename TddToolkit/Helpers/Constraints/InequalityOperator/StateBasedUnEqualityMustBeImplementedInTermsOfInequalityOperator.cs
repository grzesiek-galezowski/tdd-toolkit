﻿using System;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Collections.Generic;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class StateBasedUnEqualityMustBeImplementedInTermsOfInequalityOperator<T>
    : IConstraint where T : class
  {
    private readonly ValueObjectActivator<T> _activator;
    private int[] _indexesOfConstructorArgumentsToSkip;

    public StateBasedUnEqualityMustBeImplementedInTermsOfInequalityOperator(
      ValueObjectActivator<T> activator, int[] indexesOfConstructorArgumentsToSkip)
    {
      _activator = activator;
      this._indexesOfConstructorArgumentsToSkip = indexesOfConstructorArgumentsToSkip;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      for (var i = 0; i < _activator.GetConstructorParametersCount(); ++i)
      {
        if (ArgumentIsPartOfValueIdentity(i))
        {
          var instance2 = _activator.CreateInstanceAsValueObjectWithModifiedParameter(i);

          RecordedAssertions.True(Are.NotEqualInTermsOfInEqualityOperator(instance1, instance2), 
            "a != b should return true if both are created with different argument" + i, violations);
          RecordedAssertions.True(Are.NotEqualInTermsOfInEqualityOperator(instance1, instance2), 
            "b != a should return true if both are created with different argument" + i, violations);
        }
      }
    }

    private bool ArgumentIsPartOfValueIdentity(int i)
    {
      return !this._indexesOfConstructorArgumentsToSkip.Contains(i);
    }
  }
}