using System.Collections.Generic;
using TypeReflection.Interfaces;

namespace TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public interface ConstructorRetrieval
  {
    IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors);
  }
}