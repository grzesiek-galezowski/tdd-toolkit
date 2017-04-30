using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Castle.Components.DictionaryAdapter.Xml;
using Castle.DynamicProxy;
using NSubstitute;
using Ploeh.AutoFixture;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Subgenerators
{
  public interface IProxyBasedGenerator
  {
    T InstanceOf<T>();
    T Instance<T>();
    T OtherThan<T>(params T[] omittedValues);
    object ResultOfGenericVersionOfMethod<T>(Type type1, Type type2, string name);
    object ResultOfGenericVersionOfMethod(Type type, string name);
  }

  public class ProxyBasedGenerator : IProxyBasedGenerator
  {
    private readonly ProxyGenerator _proxyGenerator;
    private readonly FakeChainFactory _fakeChainFactory;
    private readonly Fixture _emptyCollectionFixture;
    private readonly GenericMethodProxyCalls _genericMethodProxyCalls;
    private readonly EmptyCollectionGenerator _emptyCollectionGenerator;

    public ProxyBasedGenerator(
      Fixture emptyCollectionFixture, 
      GenericMethodProxyCalls genericMethodProxyCalls, 
      EmptyCollectionGenerator emptyCollectionGenerator, 
      ProxyGenerator proxyGenerator, 
      FakeChainFactory fakeChainFactory)
    {
      _emptyCollectionFixture = emptyCollectionFixture;
      _proxyGenerator = proxyGenerator;
      _fakeChainFactory = fakeChainFactory;
      _genericMethodProxyCalls = genericMethodProxyCalls;
      _emptyCollectionGenerator = emptyCollectionGenerator;
    }

    public T InstanceOf<T>()
    {
      return _fakeChainFactory.GetInstance<T>().Resolve(this);
    }

    public T Instance<T>()
    {
      return InstanceOf<T>();
    }

    public T Dummy<T>()
    {
      var fakeInterface = _fakeChainFactory.CreateFakeOrdinaryInterfaceGenerator<T>();

      if (typeof(T).IsPrimitive)
      {
        return _fakeChainFactory.GetUnconstrainedInstance<T>().Resolve(this);
      }
      if (typeof(T) == typeof(string))
      {
        return _fakeChainFactory.GetUnconstrainedInstance<T>().Resolve(this);
      }
      if (TypeOf<T>.IsImplementationOfOpenGeneric(typeof (IEnumerable<>)))
      {
        return _emptyCollectionFixture.Create<T>();
      }
      if (TypeOf<T>.IsOpenGeneric(typeof(IEnumerable<>)))
      {
        return (T) _emptyCollectionGenerator.EmptyEnumerableOf(typeof(T).GetCollectionItemType());
      }
      if (typeof(T).IsAbstract)
      {
        return default(T);
      }
      if (fakeInterface.Applies())
      {
        return fakeInterface.Apply(this);
      }
      return (T)FormatterServices.GetUninitializedObject(typeof (T));
    }

    public T SubstituteOf<T>() where T : class
    {
      var type = typeof (T);
      var sub = Substitute.For<T>();

      var methods = SmartType.For(type).GetAllPublicInstanceMethodsWithReturnValue();

      foreach (var method in methods)
      {
        method.InvokeWithAnyArgsOn(sub, type1 => Instance(type1)).ReturnsForAnyArgs(method.GenerateAnyReturnValue(type1 => Instance(type1)));
      }
      return sub;
    }

    public T OtherThan<T>(params T[] omittedValues)
    {
      if (omittedValues == null)
      {
        return Instance<T>();
      }

      T currentValue;
      do
      {
        currentValue = Instance<T>();
      } while (omittedValues.Contains(currentValue));

      return currentValue;
    }

    public object ResultOfGenericVersionOfMethod<T>(Type type, string name)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod<T>(type, name);
    }

    public T Exploding<T>() where T : class
    {
      if (typeof(T).IsInterface)
      {
        return _proxyGenerator.CreateInterfaceProxyWithoutTarget<T>(new ExplodingInterceptor());
      }
      else
      {
        throw new Exception("Exploding instances can be created out of interfaces only!");
      }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return OtherThan(omittedValues.Cast<T>().ToArray());
    }

    public object InstanceOf(Type type)
    {
      return ResultOfGenericVersionOfMethod<Any>(type, MethodBase.GetCurrentMethod().Name);
    }

    public object Instance(Type type)
    {
      return ResultOfGenericVersionOfMethod<Any>(type, MethodBase.GetCurrentMethod().Name);
    }

    //bug remove
    public object ResultOfGenericVersionOfMethod<T>(Type type, string name, object[] args)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod<T>(type, name, args);
    }

    public object ResultOfGenericVersionOfMethod<T>(Type type1, Type type2, string name)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod<Any>(type1, type2, name);
    }

    object IProxyBasedGenerator.ResultOfGenericVersionOfMethod(Type type, string name)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(type, name);
    }
  }
}