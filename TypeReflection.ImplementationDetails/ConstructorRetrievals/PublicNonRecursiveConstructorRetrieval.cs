using System.Collections.Generic;
using System.Linq;
using TypeReflection.Interfaces;

namespace TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public class PublicNonRecursiveConstructorRetrieval : ConstructorRetrieval
  {
    private readonly ConstructorRetrieval _next;

    public PublicNonRecursiveConstructorRetrieval(ConstructorRetrieval next)
    {
      _next = next;
    }

    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      var publicConstructors = constructors.TryToObtainPublicConstructorsWithoutRecursiveArguments();

      if (publicConstructors.Any())
      {
        return publicConstructors;
      }
      else
      {
        return _next.RetrieveFrom(constructors);
      }
    }
  }
}