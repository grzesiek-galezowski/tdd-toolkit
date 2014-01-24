using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Depascalization
{
  public static class CollectionElementExtensions
  {
    public static bool IsValueOfItsAttribute(this XElement element, string attributeName,  IEnumerable<string> oneOf)
    {
      return element.Attribute(attributeName).Value.ExistsIn(oneOf);
    }

    private static bool ExistsIn<T>(this T element, IEnumerable<T> elements)
    {
      return elements.Contains(element);
    }
  }
}