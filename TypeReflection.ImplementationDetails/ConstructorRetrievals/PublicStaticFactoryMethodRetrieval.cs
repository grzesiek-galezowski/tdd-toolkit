using System.Collections.Generic;
using System.Linq;
using TypeReflection.Interfaces;

namespace TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public class PublicStaticFactoryMethodRetrieval : ConstructorRetrieval
  {
    private readonly ConstructorRetrieval _next;

    public PublicStaticFactoryMethodRetrieval(ConstructorRetrieval next)
    {
      _next = next;
    }

    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      var methods = constructors.TryToObtainPublicStaticFactoryMethodWithoutRecursion();
      if (methods.Count() == 0)
      {
        return _next.RetrieveFrom(constructors);
      }
      return methods;
    }
  }
}