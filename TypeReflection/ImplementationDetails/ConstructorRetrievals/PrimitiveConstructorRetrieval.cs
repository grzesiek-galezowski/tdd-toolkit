using System.Collections.Generic;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public class PrimitiveConstructorRetrieval : ConstructorRetrieval
  {
    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      return constructors.TryToObtainPrimitiveTypeConstructor();
    }
  }
}