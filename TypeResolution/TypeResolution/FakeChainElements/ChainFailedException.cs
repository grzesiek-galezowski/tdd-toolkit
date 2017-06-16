using System;

namespace TypeResolution.TypeResolution.FakeChainElements
{
  internal class ChainFailedException : Exception
  {
    public ChainFailedException(Type type)
      : base("Chain failed while trying to create " + type)
    {
     
    }
  }
}