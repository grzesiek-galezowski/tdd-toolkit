using System.Collections.Generic;
using System.Linq;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  class InternalRecursiveConstructorRetrieval : ConstructorRetrieval
  {
    private readonly ConstructorRetrieval _next;

    public InternalRecursiveConstructorRetrieval(ConstructorRetrieval next)
    {
      _next = next;
    }

    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      var foundConstructors = constructors.TryToObtainInternalConstructorsWithRecursiveArguments();
      if (foundConstructors.Any())
      {
        return foundConstructors.ToArray();
      }
      else
      {
        return _next.RetrieveFrom(constructors);
      }
    }
  }
}