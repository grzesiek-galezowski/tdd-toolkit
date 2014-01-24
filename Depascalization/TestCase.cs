using System.Linq;
using System.Xml.Linq;

namespace Depascalization
{
  public class TestCase
  {
    private readonly XElement _xElement;

    public TestCase(XElement xElement)
    {
      _xElement = xElement;
    }

    public void HumanizeName()
    {
      var nameText = _xElement.Attribute("name").Value;

      var nameTextParts = nameText.Split('.');

      for (var i = 0; i < nameTextParts.Length - 1; ++i)
      {
        nameTextParts[i] = nameTextParts[i].Replace("Specification", " Specification");
      }

      nameTextParts[nameTextParts.Length - 1] = new Depascalization().Of(nameTextParts[nameTextParts.Length - 1]);

      _xElement.SetAttributeValue("name", nameTextParts.Aggregate((str1, str2) => str1 + ". " + str2));
    }
  }
}