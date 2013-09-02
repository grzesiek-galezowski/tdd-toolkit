using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class ValueTypeReturnValue : AnyReturnValue
  {
    private readonly Type _type;

    private ValueTypeReturnValue(Type type)
    {
      _type = type;
    }

    public new static AnyReturnValue Of(Type type)
    {
      return new ValueTypeReturnValue(type);
    }

    public override object Generate()
    {
      return Any.ValueOf(_type);
    }
  }
}