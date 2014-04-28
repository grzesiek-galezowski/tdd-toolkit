using System;
using System.Reflection;
using Ploeh.AutoFixture;
using System.Linq;
using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    private static readonly ArrayElementPicking _arrayElementPicking = new ArrayElementPicking();
    private static readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();

    static Any()
    {
      Generator.Register(() => Types.Next());
      Generator.Register(() => MethodList.Next());
      Generator.Register(() => new Exception(Any.String(), new Exception(Any.String())));
      Generator.Customize(new MultipleCustomization());
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
      return Generator.Create<T>();
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

    public static WrapperDuo<T> WrapperOver<T>(
      T interfaceImplementation) where T : class
    {
      return WrapperOver<T>(interfaceImplementation, _ => { });
    }

    public static WrapperDuo<T> WrapperOver<T>(
      T interfaceImplementation, Action<T> setup) where T : class
    {
      var wrappingInterceptor = new WrappingInterceptor(new InterfaceInterceptor(CachedGeneration));
      setup(interfaceImplementation);

      if (typeof(T).IsInterface)
      {
        
        return WrapperDuo<T>.With(
          interfaceImplementation,
          _proxyGenerator.CreateInterfaceProxyWithTarget<T>(
            interfaceImplementation, 
            wrappingInterceptor),
          wrappingInterceptor
        );
      }
      else
      {
        return WrapperDuo<T>.With(
          interfaceImplementation,
          _proxyGenerator.CreateClassProxyWithTarget<T>(
            interfaceImplementation, 
            wrappingInterceptor),
          wrappingInterceptor
        );
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
      return FakeChain<T>.NewInstance(CachedGeneration, NestingLimit, _proxyGenerator).Resolve();
    }

    public static T Instance<T>()
    {
      return FakeChain<T>.NewInstance(CachedGeneration, NestingLimit, _proxyGenerator).Resolve();
    }

    private static T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return OtherThan(omittedValues.Cast<T>().ToArray());
    }

    public static T OtherThan<T>(params T[] omittedValues)
    {
      if(object.ReferenceEquals(omittedValues, null))
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

    public static string UrlString()
    {
      return Uri().ToString();
    }

    public static Exception Exception()
    {
      return Any.ValueOf<Exception>();
    }

    public static int Port()
    {
      return RandomGenerator.Next(65535);
    }

    public static string Ip()
    {
      return RandomGenerator.Next(256) + "."
            + RandomGenerator.Next(256) + "."
            + RandomGenerator.Next(256) + "."
            + RandomGenerator.Next(256);
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

