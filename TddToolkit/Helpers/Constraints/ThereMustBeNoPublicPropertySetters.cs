using System;
using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class ThereMustBeNoPublicPropertySetters : IConstraint
  {
    private Type _type;
    
    public ThereMustBeNoPublicPropertySetters(Type type)
    {
      _type = type;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var properties = TypeWrapper.For(_type).GetAllInstanceProperties();

      foreach (var item in properties.Where(item => item.HasPublicSetter()))
      {
        violations.Add(item.ShouldNotBeMutableButIs());
      }
    }
  }
}