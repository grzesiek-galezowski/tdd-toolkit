using System;
using System.Reflection;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails
{
  public class PropertyWrapper : IPropertyWrapper
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

    public bool HasAbstractGetter()
    {
      return _propertyInfo.GetGetMethod().IsAbstract;
    }

    public Type PropertyType { get { return _propertyInfo.PropertyType; } }

    public void SetValue(object result, object value)
    {
      _propertyInfo.SetValue(result, value, null);
    }
  }
}