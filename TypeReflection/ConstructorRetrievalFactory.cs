using System.Collections.Generic;
using TddEbook.TypeReflection.ImplementationDetails.ConstructorRetrievals;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection
{
  static internal class ConstructorRetrievalFactory
  {
    public static ConstructorRetrieval Create()
    {
      return 
        PublicNonRecursiveConstructors(
          PublicParameterlessConstructors(
            InternalParameterlessConstructorsWithoutRecursion(
              InternalParameterlessConstructors()
              )));
    }

    private static PublicNonRecursiveConstructorRetrieval PublicNonRecursiveConstructors(ConstructorRetrieval next)
    {
      return new PublicNonRecursiveConstructorRetrieval(next);
    }

    private static PublicParameterlessConstructorRetrieval PublicParameterlessConstructors(ConstructorRetrieval next)
    {
      return new PublicParameterlessConstructorRetrieval(next);
    }

    private static InternalConstructorWithoutRecursionRetrieval InternalParameterlessConstructorsWithoutRecursion(ConstructorRetrieval next)
    {
      return new InternalConstructorWithoutRecursionRetrieval(next);
    }

    private static NonPublicParameterlessConstructorRetrieval InternalParameterlessConstructors()
    {
      return new NonPublicParameterlessConstructorRetrieval();
    }
  }
}