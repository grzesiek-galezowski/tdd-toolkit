using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class ValueTypeReturnValue : AnyInstanceReturnValue
  {
    private readonly Type _type;

    private ValueTypeReturnValue(Type type)
    {
      _type = type;
    }

    public new static AnyInstanceReturnValue New(Type type)
    {
      return new ValueTypeReturnValue(type);
    }

    public override object Generate()
    {
      return Any.ValueOf(_type);
    }
  }
}