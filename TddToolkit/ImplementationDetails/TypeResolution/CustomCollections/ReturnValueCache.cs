using System.Collections.Generic;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections
{
  internal class ReturnValueCache
  {
    private readonly Dictionary<ReturnValueCacheKey, object> _cache = new Dictionary<ReturnValueCacheKey, object>();
    
    public bool AlreadyContainsValueFor(ReturnValueCacheKey cacheKey)
    {
      return _cache.ContainsKey(cacheKey);
    }

    public void Add(ReturnValueCacheKey cacheKey, object returnValue)
    {
      _cache.Add(cacheKey, returnValue);
    }

    public object ValueFor(ReturnValueCacheKey cacheKey)
    {
      return _cache[cacheKey];
    }
  }
}