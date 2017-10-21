using System;

namespace TddEbook.TddToolkit.Generators
{
  public class SpecificTypeObjectGenerator
  {
    private readonly ValueGenerator _allGeneratorValueGenerator;

    public SpecificTypeObjectGenerator(ValueGenerator valueGenerator)
    {
      _allGeneratorValueGenerator = valueGenerator;
    }

    public Uri Uri()
    {
      return _allGeneratorValueGenerator.ValueOf<Uri>();
    }

    public Guid Guid()
    {
      return _allGeneratorValueGenerator.ValueOf<Guid>();
    }
  }
}