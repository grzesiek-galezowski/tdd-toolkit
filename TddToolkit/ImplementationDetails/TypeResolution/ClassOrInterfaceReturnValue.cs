using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class ClassOrInterfaceReturnValue : AnyInstanceReturnValue
  {
    private readonly Type _type;

    private ClassOrInterfaceReturnValue(Type type)
    {
      _type = type;
    }

    public new static AnyInstanceReturnValue New(Type type)
    {
      return new ClassOrInterfaceReturnValue(type);
    }

    public override object Generate()
    {
      return Any.InstanceOf(_type);
    }
  }
}