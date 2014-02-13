using System.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection
{
  public class PropertyWrapper
  {
    private readonly PropertyInfo _propertyInfo;

    public PropertyWrapper(PropertyInfo propertyInfo)
    {
      _propertyInfo = propertyInfo;
    }

    public bool HasPublicSetter()
    {
      return _propertyInfo.GetSetMethod() != null && _propertyInfo.GetSetMethod().IsPublic;
    }

    public string ShouldNotBeMutableButIs()
    {
      return "Value objects are immutable by design, but Property "
             + _propertyInfo.Name
             + " is mutable. Declare property setter as private to pass this check";
    }
  }
}