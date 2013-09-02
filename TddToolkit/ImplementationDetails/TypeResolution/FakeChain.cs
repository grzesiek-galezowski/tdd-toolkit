using System;
using System.Collections.Generic;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class FakeChain<T> where T : class
  {
    public static FakeChain<T> NewInstance(CachedGeneration cachedGeneration, NestingLimit nestingLimit)
    {
      return new FakeChain<T>(
        nestingLimit,
        new ChainElement<T>(
          new FakeType<T>(),
          new ChainElement<T>(
            new FakeEnumerableOf<T>(),
            new ChainElement<T>(
              new FakeOrdinaryInterface<T>(cachedGeneration),
              new ChainElement<T>(
                new FakeAbstractClass<T>(),
                new ChainElement<T>(
                  new FakeConcreteClassWithNonConcreteConstructor<T>(),
                  new ChainElement<T>(
                    new FakeConcreteClass<T>(),
                    new NullChainElement<T>())))))));
    }

    private readonly NestingLimit _nestingLimit;
    private readonly IChainElement<T> _chainHead;

    public FakeChain(NestingLimit nestingLimit, IChainElement<T> chainHead)
    {
      _nestingLimit = nestingLimit;
      _chainHead = chainHead;
    }

    public T Resolve()
    {
      try
      {
        _nestingLimit.AddNesting();
        if (!_nestingLimit.IsReached())
        {
          return _chainHead.Resolve();
        }
        else
        {
          return default(T);
        }
      }
      finally
      {
        _nestingLimit.RemoveNesting();
      }
    }
  }

  internal class FakeConcreteClassWithNonConcreteConstructor<T> : IResolution<T>
  {
    public bool Applies()
    {
      return ConstructorHasAtLeastOneNonConcreteArgumentType(typeof(T));
    }

    private bool ConstructorHasAtLeastOneNonConcreteArgumentType(Type type)
    {
      var constructor = PickConstructorWithLeastNonPointersParametersFrom(type);
      return constructor.HasAbstractOrInterfaceArguments();
    }

    public T Apply()
    {
      return (T)UseFallbackMechanismForConstructorWithInterfaceOrAbstractParameters(typeof(T));
    }

    private object UseFallbackMechanismForConstructorWithInterfaceOrAbstractParameters(Type type)
    {
      var constructor = PickConstructorWithLeastNonPointersParametersFrom(type);
      var constructorValues = constructor.GenerateAnyParameterValues();
      var instance = Activator.CreateInstance(type, constructorValues.ToArray());
      return instance;
    }

    private static TypeConstructor PickConstructorWithLeastNonPointersParametersFrom(Type type)
    {
      var constructors = TypeConstructor.ExtractAllFrom(type);
      TypeConstructor leastParamsConstructor = null;
      var numberOfParams = int.MaxValue;

      foreach (var typeConstructor in constructors)
      {
        if (typeConstructor.HasNonPointerArgumentsOnly() && typeConstructor.HasLessParametersThan(numberOfParams))
        {
          leastParamsConstructor = typeConstructor;
          numberOfParams = typeConstructor.GetParametersCount();
        }
      }
      return leastParamsConstructor;
    }
  }
}