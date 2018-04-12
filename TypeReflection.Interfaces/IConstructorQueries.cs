using System.Collections.Generic;
using TddEbook.TddToolkit.CommonTypes;

namespace TypeReflection.Interfaces
{
  public interface IConstructorQueries
  {
    Maybe<IConstructorWrapper> GetNonPublicParameterlessConstructorInfo();
    Maybe<IConstructorWrapper> GetPublicParameterlessConstructor();
    List<IConstructorWrapper> TryToObtainInternalConstructorsWithoutRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainPublicConstructorsWithoutRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainPublicConstructorsWithRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainInternalConstructorsWithRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainPrimitiveTypeConstructor();
    IEnumerable<IConstructorWrapper> TryToObtainPublicStaticFactoryMethodWithoutRecursion();
  }
}