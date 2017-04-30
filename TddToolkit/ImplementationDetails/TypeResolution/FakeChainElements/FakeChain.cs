using System.Collections;
using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors;
using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public interface IFakeChain<out T>
  {
    T Resolve(IProxyBasedGenerator proxyBasedGenerator);
  }

  internal class FakeChain<T> : IFakeChain<T>
  {
    private readonly IChainElement<T> _chainHead;

    public static IFakeChain<T> NewInstance(CachedReturnValueGeneration eachMethodReturnsTheSameValueOnEveryCall, NestingLimit nestingLimit, ProxyGenerator generationIsDoneUsingProxies)
    {
      return LimitedTo(nestingLimit,
        UnconstrainedInstance(eachMethodReturnsTheSameValueOnEveryCall, generationIsDoneUsingProxies));
    }

    public static FakeChain<T> UnconstrainedInstance(CachedReturnValueGeneration eachMethodReturnsTheSameValueOnEveryCall, ProxyGenerator generationIsDoneUsingProxies)
    {
      return OrderedChainOfGenerationsWithTheFollowingLogic(
        TryTo(ResolveTheMostSpecificCases(),
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
                                              ElseTryTo(ResolveAsConcreteClass(),
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

    private static FakeConcreteClass<T> ResolveAsConcreteClass()
    {
      return new FakeConcreteClass<T>(new FallbackTypeGenerator<T>());
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

    private static IResolution<T> ResolveAsGenericEnumerator()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfGenericEnumerator();
    }

    private static IResolution<T> ResolveAsKeyValuePair()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfKeyValuePair();
    }

    private static IResolution<T> ResolveAsSortedDictionary()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfSortedDictionary();
    }

    private static IResolution<T> ResolveAsConcurrentStack()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfConcurrentStack();
    }

    private static IResolution<T> ResolveAsConcurrentQueue()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfConcurrentQueue();
    }

    private static IResolution<T> ResolveAsConcurrentBag()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfConcurrentBag();
    }

    private static IResolution<T> ResolveAsConcurrentDictionary()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfConcurrentDictionary();
    }

    private static IResolution<T> ResolveAsSortedSet()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfSortedSet();
    }

    private static IResolution<T> ResolveAsSortedList()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfSortedList();
    }

    private static ResolutionOfTypeWithGenerics<T> ResolveAsSimpleDictionary()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfSimpleDictionary();
    }

    private static ResolutionOfTypeWithGenerics<T> ResolveAsSimpleSet()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfSimpleSet();
    }

    private static ResolutionOfTypeWithGenerics<T> ResolveAsSimpleEnumerableAndList()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfSimpleIEnumerableAndList();
    }

    private static FakeSpecialCase<T> ResolveTheMostSpecificCases()
    {
      return new FakeSpecialCase<T>();
    }

    private static InvalidChainElement<T> ElseReportUnsupportedType()
    {
      return new InvalidChainElement<T>();
    }

    private static ChainElement<T> ElseTryTo(IResolution<T> handleArraysInSpecialWay, IChainElement<T> chainElement)
    {
      return new ChainElement<T>(handleArraysInSpecialWay, chainElement);
    }

    private static IChainElement<T> TryTo(FakeSpecialCase<T> fakeSpecialCase, IChainElement<T> chainElement)
    {
      return new ChainElement<T>(fakeSpecialCase, chainElement);
    }

    private static IResolution<T> ResolveAsArray()
    {
      return SpecialCasesOfResolutions<T>.CreateResolutionOfArray();
    }


    public FakeChain(IChainElement<T> chainHead)
    {
      _chainHead = chainHead;
    }

    public T Resolve(IProxyBasedGenerator proxyBasedGenerator)
    {
      return _chainHead.Resolve(proxyBasedGenerator);
    }
  }

  public class FakeEnumerator<T> : IResolution<T>
  {
    public bool Applies()
    {
      return TypeOf<T>.Is<IEnumerator>();
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      return (T)(Any.Array<object>().GetEnumerator());
    }
  }
}