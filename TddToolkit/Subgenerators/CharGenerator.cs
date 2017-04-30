using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class CharGenerator
  {
    private readonly CircularList<char> _letters =
      CircularList.CreateStartingFromRandom("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());

    private readonly CircularList<char> _digitChars =
      CircularList.CreateStartingFromRandom("5647382910".ToCharArray());

    private readonly ValueGenerator _valueGenerator;

    public CharGenerator(ValueGenerator valueGenerator)
    {
      _valueGenerator = valueGenerator;
    }

    public char AlphaChar() => 
      _letters.Next();

    public char DigitChar() => 
      _digitChars.Next();

    public char Char() => _valueGenerator.ValueOf<char>();
    public char LowerCaseAlphaChar() => char.ToLower(AlphaChar());
    public char UpperCaseAlphaChar() => char.ToUpper(AlphaChar());
  }
}