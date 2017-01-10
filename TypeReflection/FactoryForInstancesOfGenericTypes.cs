using System;

namespace TddEbook.TypeReflection
{
  public interface FactoryForInstancesOfGenericTypes
  {
    object NewInstanceOf(Type type);
  }
}
