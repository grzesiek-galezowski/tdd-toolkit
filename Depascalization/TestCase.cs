using System.Linq;
using System.Xml.Linq;

namespace Depascalization
{
  public class TestCase
  {
    private readonly XElement _xElement;
    private readonly Transformation _transformation;

    public TestCase(XElement xElement)
    {
      _xElement = xElement;
      _transformation = new Transformation();
    }

    public void HumanizeName()
    {
      var nameText = _xElement.Attribute("name").Value;

      var nameTextParts = nameText.Split('.');

      for (var i = 0; i < nameTextParts.Length - 1; ++i)
      {
        nameTextParts[i] = nameTextParts[i].Replace("Specification", " Specification");
      }

      nameTextParts[nameTextParts.Length - 1] = _transformation.Of(nameTextParts[nameTextParts.Length - 1]);

      _xElement.SetAttributeValue("name", nameTextParts.Aggregate((str1, str2) => str1 + ". " + str2));
    }
  }
}