using TddEbook.TypeReflection.ImplementationDetails.ConstructorRetrievals;

namespace TddEbook.TypeReflection
{
  public class ConstructorRetrievalFactory
  {
    private readonly ConstructorRetrieval _constructorQuery;

    public ConstructorRetrievalFactory()
    {
      _constructorQuery = 
        PublicNonRecursiveConstructors(
          PublicParameterlessConstructors(
            InternalNonRecursiveConstructors(
              InternalParameterlessConstructors(
                PublicStaticNonRecursiveFactoryMethod(
                  PublicRecursiveConstructors(
                    InternalRecursiveConstructors(
                      PrimitiveConstructor()
                    )))))));
    }

    private ConstructorRetrieval PublicStaticNonRecursiveFactoryMethod(ConstructorRetrieval next)
    {
      return new PublicStaticFactoryMethodRetrieval(next);
    }

    private ConstructorRetrieval PrimitiveConstructor()
    {
      return new PrimitiveConstructorRetrieval();
    }

    public ConstructorRetrieval Create()
    {
      return _constructorQuery;
    }

    private static ConstructorRetrieval PublicRecursiveConstructors(ConstructorRetrieval next)
    {
      return new PublicRecursiveConstructorsRetrieval(next);
    }

    private static ConstructorRetrieval InternalRecursiveConstructors(ConstructorRetrieval next)
    {
      return new InternalRecursiveConstructorRetrieval(next);
    }

    private static ConstructorRetrieval PublicNonRecursiveConstructors(ConstructorRetrieval next)
    {
      return new PublicNonRecursiveConstructorRetrieval(next);
    }

    private static ConstructorRetrieval PublicParameterlessConstructors(ConstructorRetrieval next)
    {
      return new PublicParameterlessConstructorRetrieval(next);
    }

    private static ConstructorRetrieval InternalNonRecursiveConstructors(ConstructorRetrieval next)
    {
      return new InternalConstructorWithoutRecursionRetrieval(next);
    }

    private static ConstructorRetrieval InternalParameterlessConstructors(ConstructorRetrieval next)
    {
      return new NonPublicParameterlessConstructorRetrieval(next);
    }
  }
}