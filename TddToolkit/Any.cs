using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NSubstitute;
using Ploeh.AutoFixture;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors;
using TddEbook.TypeReflection;
using Type = System.Type;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    private static readonly ArrayElementPicking _arrayElementPicking;
    private static readonly ProxyGenerator _proxyGenerator;
    private static readonly FakeChainFactory FakeChainFactory;
    private static readonly CachedReturnValueGeneration _cachedGeneration;

    static Any()
    {
      _arrayElementPicking = new ArrayElementPicking();
      _cachedGeneration = new CachedReturnValueGeneration(new PerMethodCache<object>());
      _proxyGenerator = new ProxyGenerator();
      FakeChainFactory = new FakeChainFactory(_cachedGeneration, _nestingLimit, _proxyGenerator);
      _generator.Register(() => _types.Next());
      _generator.Register(() => MethodList.Next());
      _generator.Register(() => new Exception(String(), new Exception(String())));
      _generator.Register(() => new IPAddress(new byte[] { Any.Octet(), Any.Octet(), Any.Octet(), Any.Octet() }));
      _generator.Customize(new MultipleCustomization());
    }

    public static IPAddress IpAddress()
    {
      return _generator.Create<IPAddress>();
    }

    public static T ValueOtherThan<T>(params T[] omittedValues)
    {
      T currentValue;
      do
      {
        currentValue = ValueOf<T>();
      } while (omittedValues.Contains(currentValue));

      return currentValue;
    }

    public static T From<T>(params T[] possibleValues)
    {
      var latestArraysWithPossibleValues = _arrayElementPicking.For<T>();

      if (!latestArraysWithPossibleValues.Contain(possibleValues))
      {
        latestArraysWithPossibleValues.Add(possibleValues);
      }

      var result = latestArraysWithPossibleValues.PickNextElementFrom(possibleValues);
      return result;
    }

    public static DateTime DateTime()
    {
      return ValueOf<DateTime>();
    }

    public static TimeSpan TimeSpan()
    {
      return ValueOf<TimeSpan>();
    }

    public static T ValueOf<T>()
    {
      //bug: add support for creating generic structs with interfaces
      return _generator.Create<T>();
    }

    public static string LegalXmlTagName()
    {
      return Identifier();
    }

    public static bool Boolean()
    {
      return ValueOf<bool>();
    }

    public static object Object()
    {
      return ValueOf<object>();
    }

    public static T Exploding<T>() where T : class
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

    public static MethodInfo Method()
    {
      return ValueOf<MethodInfo>();
    }

    public static Type Type()
    {
      return ValueOf<Type>();
    }

    public static T InstanceOf<T>()
    {
      return FakeChainFactory.GetInstance<T>().Resolve();
    }

    public static T Instance<T>()
    {
      return InstanceOf<T>();
    }

    public static T Dummy<T>()
    {
      if (typeof(T).IsPrimitive)
      {
        return FakeChainFactory.GetUnconstrainedInstance<T>().Resolve();
      }
      if (typeof(T) == typeof(string))
      {
        return FakeChainFactory.GetUnconstrainedInstance<T>().Resolve();
      }
      if (typeof(T).IsAbstract)
      {
        return default(T);
      }
      if (typeof(T).IsInterface)
      {
        return default(T);
      }
      return (T)FormatterServices.GetUninitializedObject(typeof (T));
    }



#pragma warning disable CC0068 // Unused Method
#pragma warning disable S1144 // Unused private types or members should be removed
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private static T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return OtherThan(omittedValues.Cast<T>().ToArray());
    }
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore CC0068 // Unused Method

    public static T SubstituteOf<T>() where T : class
    {
      var type = typeof (T);
      var sub = Substitute.For<T>();

      var methods = TypeReflection.SmartType.For(type).GetAllPublicInstanceMethodsWithReturnValue();

      foreach (var method in methods)
      {
        method.InvokeWithAnyArgsOn(sub, Instance).ReturnsForAnyArgs(method.GenerateAnyReturnValue(Instance));
      }
      return sub;
    }

    public static T OtherThan<T>(params T[] omittedValues)
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

    public static Uri Uri()
    {
      return ValueOf<Uri>();
    }

    public static Guid Guid()
    {
      return ValueOf<Guid>();
    }

    public static string UrlString()
    {
      return Uri().ToString();
    }

    public static Exception Exception()
    {
      return ValueOf<Exception>();
    }

    public static int Port()
    {
      return _randomGenerator.Next(65535);
    }

    public static string Ip()
    {
      return _randomGenerator.Next(256) + "."
            + _randomGenerator.Next(256) + "."
            + _randomGenerator.Next(256) + "."
            + _randomGenerator.Next(256);
    }

    public static object InstanceOf(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object Instance(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }


    public static object ValueOf(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object InstanceOtherThanObjects(Type type, params object[] omittedValues)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name, omittedValues);
    }

    public static IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      var list = new List<T>();
      list.Add(Instance<T>());
      list.AddRange(included);
      list.Add(Instance<T>());

      return list;
    }

    public static Task NotStartedTask()
    {
      return new Task(() => Task.Delay(1).Wait());
    }

    public static Task<T> NotStartedTask<T>()
    {
      return new Task<T>(Instance<T>);
    }

    public static Task StartedTask()
    {
      return Clone.Of(Task.Delay(0));
    }

    public static Task<T> StartedTask<T>()
    {
      return Task.FromResult(Instance<T>());
    }

    public static Func<T> Func<T>()
    {
      return Any.Instance<Func<T>>();
    }
    public static Func<T1, T2> Func<T1, T2>()
    {
      return Any.Instance<Func<T1, T2>>();
    }

    public static Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return Any.Instance<Func<T1, T2, T3>>();
    }

    public static Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return Any.Instance<Func<T1, T2, T3, T4>>();
    }

    public static Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return Any.Instance<Func<T1, T2, T3, T4, T5>>();
    }

    public static Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return Any.Instance<Func<T1, T2, T3, T4, T5, T6>>();
    }

    public static Action Action()
    {
      return Any.Instance<Action>();
    }

    public static Action<T> Action<T>()
    {
      return Any.Instance<Action<T>>();
    }
    public static Action<T1, T2> Action<T1, T2>()
    {
      return Any.Instance<Action<T1, T2>>();
    }

    public static Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return Any.Instance<Action<T1, T2, T3>>();
    }

    public static Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return Any.Instance<Action<T1, T2, T3, T4>>();
    }

    public static Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return Any.Instance<Action<T1, T2, T3, T4, T5>>();
    }

    public static Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return Any.Instance<Action<T1, T2, T3, T4, T5, T6>>();
    }


  }

  public class Type1 { }
  public class Type2 { }
  public class Type3 { }
  public class Type4 { }
  public class Type5 { }
  public class Type6 { }
  public class Type7 { }
  public class Type8 { }
  public class Type9 { }
  public class Type10 { }
  public class Type11 { }
  public class Type12 { }
  public class Type13 { }

}

