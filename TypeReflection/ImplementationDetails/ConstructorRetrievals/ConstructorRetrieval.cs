using System.Collections.Generic;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public interface ConstructorRetrieval
  {
    IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors);
  }
}