using System;
using Ploeh.AutoFixture;
using System.Linq;
using System.Collections.Generic;
using Ploeh.AutoFixture.Kernel;
using Castle.DynamicProxy;
using NSubstitute;
using TddEbook.TddToolkit.ImplementationDetails;

namespace TddEbook.TddToolkit
{
  public static class Any
  {
    private static readonly Fixture _generator = new Fixture();
    private static readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
    private static readonly RegularExpressionGenerator _regexGenerator = new RegularExpressionGenerator();

    public static int Integer()
    {
      return _generator.Create<int>();
    }

    public static T ValueOtherThan<T>(params T[] omittedValues)
    {
      T currentValue;
      do
      {
        currentValue = Any.ValueOf<T>();
      } while(omittedValues.Contains(currentValue));
      
      return currentValue;
    }

    public static double Double()
    {
      return _generator.Create<double>();
    }

    public static string String()
    {
      return _generator.Create<string>();
    }

    public static T From<T>(params T[] possibleValues)
    {
      var random = new Random();
      var index = random.Next(possibleValues.Length - 1);
      return possibleValues[index];
    }

    public static T Of<T>() where T : struct, IConvertible
    {
      AssertDynamicEnumConstraintFor<T>();
      
      return _generator.Create<T>();
    }

    public static T Besides<T>(params T[] excludedValues) where T : struct, IConvertible
    {
      AssertDynamicEnumConstraintFor<T>();
      return Any.ValueOtherThan(excludedValues);
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

    public static string StringMatching(string pattern)
    {
      var request = new RegularExpressionRequest(pattern);
      
      var result = _regexGenerator.Create(request, new DummyContext());
      return result.ToString();
    }

    public static string StringOfLength(int charactersCount)
    {
      var result = string.Empty;
      while(result.Count() < charactersCount)
      {
        result += Any.String();
      }
      return result.Substring(0, charactersCount);
    }

    public static string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return Any.ValueOtherThan(alreadyUsedStrings);
    }

    public static string StringNotContaining(params string[] excludedSubstrings)
    {
      var result = Any.String();
      do
      {
        result = Any.String();
      } while(excludedSubstrings.Any(result.Contains));
      return result;
    }

    public static string StringContaining(string str)
    {
      return Any.String() + str + Any.String();
    }

    public static IEnumerable<T> EnumerableOfDerivativesFrom<T>() where T : class
    {
      return new List<T>() {
        Any.InstanceOf<T>(),
        Any.InstanceOf<T>(),
        Any.InstanceOf<T>()
      };
    }

    public static IEnumerable<T> Enumerable<T>()
    {
      return _generator.CreateMany<T>();
    }

    public static IEnumerable<T> EnumerableWithout<T>(params T[] excluded) where T : class
    {
      var result = new List<T>();
      result.Add(Any.ValueOtherThan(excluded));
      result.Add(Any.ValueOtherThan(excluded));
      result.Add(Any.ValueOtherThan(excluded));
      return result;
    }

    public static T[] Array<T>()
    {
      return Any.Enumerable<T>().ToArray();
    }

    public static T[] ArrayWithout<T>(params T[] excluded) where T : class
    {
      return Any.EnumerableWithout(excluded).ToArray();
    }

    public static List<T> List<T>()
    {
      return Any.Enumerable<T>().ToList();
    }

    public static ISet<T> Set<T>()
    {
      return Any.ValueOf<HashSet<T>>();
    }

    public static Dictionary<T, U> Dictionary<T, U>()
    {
      return Any.ValueOf<Dictionary<T, U>>();
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return Any.SortedSet<T>().ToList();
    }

    public static string LegalXmlTagName()
    {
      return Any.Identifier();
    }

    public static char AlphaChar()
    {
      return _letters.Next();
    }

    static char DigitChar()
    {
      return _digits.Next();
    }

    public static int IntegerOtherThan(params int[] others)
    {
      return Any.ValueOtherThan(others);
    }

    public static byte Byte()
    {
      return Any.ValueOf<byte>();
    }

    public static byte ByteOtherThan(params byte[] others)
    {
      return Any.ValueOtherThan(others);
    }

    public static bool Boolean()
    {
      return Any.ValueOf<bool>();
    }

    public static object Object()
    {
      return Any.ValueOf<object>();
    }

    public static T Exploding<T>() where T : class
    {
      return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(new ExplodingInterceptor());
    }

    public static Type Type()
    {
      return types.Next();
    }

    public static T InstanceOf<T>() where T : class
    {
      return Substitute.For<T>(); //TODO
    }

    public static string Identifier()
    {
      string result = Any.AlphaChar().ToString();
      for(var i = 0 ; i < 5 ; ++i)
      {
        result += Any.DigitChar();
        result += Any.AlphaChar();
      }
      return result;
    }

    public static Uri Uri()
    {
      return Any.ValueOf<Uri>();
    }

    public static string UrlString()
    {
      return Any.Uri().ToString();
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

    public static string AlphaString()
    {
      return AlphaString(Any.String().Length);
    }

    static string AlphaString(int maxLength)
    {
      var result = string.Empty;
      for(var i = 0 ; i < maxLength ; ++i)
      {
        result += Any.AlphaChar();
      }
      return result;
    }

    public static SortedSet<T> SortedSet<T>()
    {
      return new SortedSet<T> {
        Any.ValueOf<T>(),
        Any.ValueOf<T>(),
        Any.ValueOf<T>()
      };
    }

    private static void AssertDynamicEnumConstraintFor<T>()
    {
      if(!typeof(T).IsEnum)
      {
        throw new ArgumentException("T must be an enumerated type");
      }
    }
    
    private static readonly CircularList<char> _letters = new CircularList<char>("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray()) ;
    private static readonly CircularList<char> _digits = new CircularList<char>("5647382910".ToCharArray());

    private static readonly CircularList<Type> types = new CircularList<Type>(
      typeof(Type1), 
      typeof(Type2), 
      typeof(Type3), 
      typeof(Type4), 
      typeof(Type5), 
      typeof(Type6), 
      typeof(Type7), 
      typeof(Type8), 
      typeof(Type9), 
      typeof(Type10), 
      typeof(Type11), 
      typeof(Type12), 
      typeof(Type13));
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

