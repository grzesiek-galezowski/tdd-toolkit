using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions
{
  internal class TypeWrapper<T>
  {
    public static IEnumerable<FieldWrapper> GetAllInstanceFields()
    {
      var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      return fields.Select(f => new FieldWrapper(f));
    }

    public static IEnumerable<PropertyWrapper> GetAllInstanceProperties()
    {
      var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
      return properties.Select(p => new PropertyWrapper(p));
    }

  }
}