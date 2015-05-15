using System.Collections.Generic;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public class NonPublicParameterlessConstructorRetrieval : ConstructorRetrieval
  {
    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      return new List<IConstructorWrapper> { new DefaultParameterlessConstructor(constructors.GetNonPublicParameterlessConstructorInfo()) };
    }
  }
}