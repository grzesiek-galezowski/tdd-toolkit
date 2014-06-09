using System;
using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Helpers.Constraints
{
  public class ThereMustBeNoPublicPropertySetters : IConstraint
  {
    private readonly Type _type;
    
    public ThereMustBeNoPublicPropertySetters(Type type)
    {
      _type = type;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var properties = TypeWrapper.For(_type).GetAllPublicInstanceProperties();

      foreach (var item in properties.Where(item => item.HasPublicSetter()))
      {
        violations.Add(item.ShouldNotBeMutableButIs());
      }
    }
  }
}