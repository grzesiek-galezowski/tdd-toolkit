using System.Xml.Linq;

namespace Depascalization
{
  public class NamespaceOrFixture
  {
    private readonly XElement _xElement;

    public NamespaceOrFixture(XElement xElement)
    {
      _xElement = xElement;
    }

    private void AddSpaceBefore(string word, string inAttribute)
    {
      var attributeName = inAttribute;
      _xElement.SetAttributeValue(
        attributeName,
        _xElement.Attribute(attributeName).Value.Replace(word, " " + word));
    }

    public void HumanizeName()
    {
      AddSpaceBefore("Specification", inAttribute: "name");
    }
  }
}