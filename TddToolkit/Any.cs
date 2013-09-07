using System;
using System.Reflection;
using Ploeh.AutoFixture;
using System.Linq;
using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;

namespace TddEbook.TddToolkit
{
  public static partial class Any
  {
    static Any()
    {
      _generator.Register<Type>(() => types.Next());
      _generator.Register<MethodInfo>(() => _methodList.Next());
    }

    public static T ValueOtherThan<T>(params T[] omittedValues)
    {
      T currentValue;
      do
      {
        currentValue = ValueOf<T>();
      } while(omittedValues.Contains(currentValue));
      
      return currentValue;
    }

    public static T From<T>(params T[] possibleValues)
    {
      var random = new Random();
      var index = random.Next(possibleValues.Length - 1);
      return possibleValues[index];
    }

    public static DateTime DateTime()
    {
      return _generator.Create<DateTime>();
    }

    public static TimeSpan TimeSpan()
    {
      return _generator.Create<TimeSpan>();
    }

    public static T ValueOf<T>()
    {
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
      if (typeof (T).IsInterface)
      {
        return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(new ExplodingInterceptor());  
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

    public static T InstanceOf<T>() where T : class
    {
      return FakeChain<T>.NewInstance(_cachedGeneration, _nestingLimit).Resolve();
    }

    public static Uri Uri()
    {
      return ValueOf<Uri>();
    }

    public static string UrlString()
    {
      return Uri().ToString();
    }

    public static int Port()
    {
      return _random.Next(65535);
    }

    public static string Ip()
    {
      return _random.Next(256) + "." 
            + _random.Next(256) + "." 
            + _random.Next(256) + "." 
            + _random.Next(256);
    }

    public static object InstanceOf(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object ValueOf(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
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

