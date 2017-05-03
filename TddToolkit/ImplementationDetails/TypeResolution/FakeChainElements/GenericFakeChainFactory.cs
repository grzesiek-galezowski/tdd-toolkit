using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors;
using TddEbook.TddToolkit.Subgenerators;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class GenericFakeChainFactory<T>
  {
    private readonly SpecialCasesOfResolutions<T> _specialCasesOfResolutions;

    public GenericFakeChainFactory(SpecialCasesOfResolutions<T> specialCasesOfResolutions)
    {
      _specialCasesOfResolutions = specialCasesOfResolutions;
    }

    public IFakeChain<T> NewInstance(
      CachedReturnValueGeneration eachMethodReturnsTheSameValueOnEveryCall, 
      NestingLimit nestingLimit, 
      ProxyGenerator generationIsDoneUsingProxies, 
      ValueGenerator valueGenerator)
    {
      return LimitedTo(nestingLimit, UnconstrainedInstance(
        eachMethodReturnsTheSameValueOnEveryCall, 
        generationIsDoneUsingProxies,
        valueGenerator));
    }

    public FakeChain<T> UnconstrainedInstance(CachedReturnValueGeneration eachMethodReturnsTheSameValueOnEveryCall, ProxyGenerator generationIsDoneUsingProxies, ValueGenerator valueGenerator)
    {
      return OrderedChainOfGenerationsWithTheFollowingLogic(TryTo(
        ResolveTheMostSpecificCases(valueGenerator), 
        ElseTryTo(ResolveAsArray(), 
          ElseTryTo(ResolveAsSimpleEnumerableAndList(), 
            ElseTryTo(ResolveAsSimpleSet(), 
              ElseTryTo(ResolveAsSimpleDictionary(), 
                ElseTryTo(ResolveAsSortedList(), 
                  ElseTryTo(ResolveAsSortedSet(), 
                    ElseTryTo(ResolveAsSortedDictionary(), 
                      ElseTryTo(ResolveAsConcurrentDictionary(), 
                        ElseTryTo(ResolveAsConcurrentBag(), 
                          ElseTryTo(ResolveAsConcurrentQueue(), 
                            ElseTryTo(ResolveAsConcurrentStack(), 
                              ElseTryTo(ResolveAsKeyValuePair(), 
                                ElseTryTo(ResolveAsGenericEnumerator(), 
                                  ElseTryTo(ResolveAsObjectEnumerator(), 
                                    ElseTryTo(ResolveAsCollectionWithHeuristics(), 
                                      ElseTryTo(ResolveAsInterfaceImplementationWhere(eachMethodReturnsTheSameValueOnEveryCall, generationIsDoneUsingProxies), 
                                        ElseTryTo(ResolveAsAbstractClassImplementationWhere(eachMethodReturnsTheSameValueOnEveryCall, generationIsDoneUsingProxies), 
                                          ElseTryTo(ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature(), 
                                            ElseTryTo(ResolveAsConcreteClass(valueGenerator), 
                                              ElseReportUnsupportedType()
                                            )))))))))))))))))))));
    }

    private static FakeChain<T> OrderedChainOfGenerationsWithTheFollowingLogic(IChainElement<T> first)
    {
      return new FakeChain<T>(first);
    }

    private static IFakeChain<T> LimitedTo(NestingLimit limit, IFakeChain<T> fakeChain)
    {
      return new LimitedFakeChain<T>(limit, fakeChain);
    }

    private static FakeConcreteClass<T> ResolveAsConcreteClass(ValueGenerator valueGenerator)
    {
      return new FakeConcreteClass<T>(
        new FallbackTypeGenerator<T>(), valueGenerator);
    }

    private static FakeConcreteClassWithNonConcreteConstructor<T> ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature()
    {
      return new FakeConcreteClassWithNonConcreteConstructor<T>();
    }

    private static FakeAbstractClass<T> ResolveAsAbstractClassImplementationWhere(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
    {
      return new FakeAbstractClass<T>(cachedGeneration, proxyGenerator);
    }

    private static FakeOrdinaryInterface<T> ResolveAsInterfaceImplementationWhere(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
    {
      return new FakeOrdinaryInterface<T>(cachedGeneration, proxyGenerator);
    }

    private static FakeUnknownCollection<T> ResolveAsCollectionWithHeuristics()
    {
      return new FakeUnknownCollection<T>();
    }

    private static FakeEnumerator<T> ResolveAsObjectEnumerator()
    {
      return new FakeEnumerator<T>();
    }

    private IResolution<T> ResolveAsGenericEnumerator()
    {
      return _specialCasesOfResolutions.CreateResolutionOfGenericEnumerator();
    }

    private IResolution<T> ResolveAsKeyValuePair()
    {
      return _specialCasesOfResolutions.CreateResolutionOfKeyValuePair();
    }

    private IResolution<T> ResolveAsSortedDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOfSortedDictionary();
    }

    private IResolution<T> ResolveAsConcurrentStack()
    {
      return _specialCasesOfResolutions.CreateResolutionOfConcurrentStack();
    }

    private IResolution<T> ResolveAsConcurrentQueue()
    {
      return _specialCasesOfResolutions.CreateResolutionOfConcurrentQueue();
    }

    private IResolution<T> ResolveAsConcurrentBag()
    {
      return _specialCasesOfResolutions.CreateResolutionOfConcurrentBag();
    }

    private IResolution<T> ResolveAsConcurrentDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOfConcurrentDictionary();
    }

    private IResolution<T> ResolveAsSortedSet()
    {
      return _specialCasesOfResolutions.CreateResolutionOfSortedSet();
    }

    private IResolution<T> ResolveAsSortedList()
    {
      return _specialCasesOfResolutions.CreateResolutionOfSortedList();
    }

    private ResolutionOfTypeWithGenerics<T> ResolveAsSimpleDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOfSimpleDictionary();
    }

    private ResolutionOfTypeWithGenerics<T> ResolveAsSimpleSet()
    {
      return _specialCasesOfResolutions.CreateResolutionOfSimpleSet();
    }

    private ResolutionOfTypeWithGenerics<T> ResolveAsSimpleEnumerableAndList()
    {
      return _specialCasesOfResolutions.CreateResolutionOfSimpleIEnumerableAndList();
    }

    private static FakeSpecialCase<T> ResolveTheMostSpecificCases(ValueGenerator valueGenerator)
    {
      return new FakeSpecialCase<T>(valueGenerator);
    }

    private static InvalidChainElement<T> ElseReportUnsupportedType()
    {
      return new InvalidChainElement<T>();
    }

    private static ChainElement<T> ElseTryTo(IResolution<T> handleArraysInSpecialWay, IChainElement<T> chainElement)
    {
      return new ChainElement<T>(handleArraysInSpecialWay, chainElement);
    }

    private static IChainElement<T> TryTo(IResolution<T> fakeSpecialCase, IChainElement<T> chainElement)
    {
      return new ChainElement<T>(fakeSpecialCase, chainElement);
    }

    private IResolution<T> ResolveAsArray()
    {
      return _specialCasesOfResolutions.CreateResolutionOfArray();
    }
  }
}