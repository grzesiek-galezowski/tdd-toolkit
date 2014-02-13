using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class ChainFailedException : Exception
  {
    public ChainFailedException(Type type)
      : base("Chain failed while trying to create " + type)
    {
     
    }
  }
}