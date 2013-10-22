using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.MethodReturnValues
{
  internal class ClassOrInterfaceReturnValue : AnyReturnValue
  {
    private readonly Type _type;

    private ClassOrInterfaceReturnValue(Type type)
    {
      _type = type;
    }

    public new static AnyReturnValue Of(Type type)
    {
      return new ClassOrInterfaceReturnValue(type);
    }

    public override object Generate()
    {
      return Any.InstanceOf(_type);
    }
  }
}