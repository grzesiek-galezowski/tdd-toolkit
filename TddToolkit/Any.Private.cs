using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.AutoFixture;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    private static readonly Fixture _generator = new Fixture();
    private static readonly Random _randomGenerator = new Random(System.Guid.NewGuid().GetHashCode());
    private static readonly RegularExpressionGenerator _regexGenerator = new RegularExpressionGenerator();

    private static readonly CachedReturnValueGeneration _cachedGeneration =
      new CachedReturnValueGeneration(new PerMethodCache<object>());

    private static readonly CircularList<char> _letters =
      CircularList.CreateStartingFromRandom("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());

    private static readonly CircularList<char> _digitChars =
      CircularList.CreateStartingFromRandom("5647382910".ToCharArray());

    private static readonly CircularList<byte> _digits =
      CircularList.CreateStartingFromRandom(new byte[] {5, 6, 4, 7, 3, 8, 2, 9, 1, 0});

    private static readonly CircularList<Type> _types = CircularList.CreateStartingFromRandom(typeof(Type1),
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

    private static readonly CircularList<MethodInfo> MethodList =
      CircularList.CreateStartingFromRandom(typeof(List<int>).GetMethods(BindingFlags.Public | BindingFlags.Instance));

    private static readonly NestingLimit _nestingLimit = GlobalNestingLimit.Of(5);


    private static object ResultOfGenericVersionOfMethod(Type type, string name)
    {
      return typeof(Any).GetMethods().Where(NameIs(name))
        .First(ParameterlessGenericVersion()).MakeGenericMethod(type).Invoke(null, null);
    }

    private static Func<MethodInfo, bool> ParameterlessGenericVersion()
    {
      return m => !m.GetParameters().Any() && m.IsGenericMethodDefinition;
    }

    private static Func<MethodInfo, bool> NameIs(string name)
    {
      return m => m.Name == name;
    }

    private static object ResultOfGenericVersionOfMethod(Type type, string name, object[] args)
    {
      var method = FindEmptyGenericsMethod(name);

      var genericMethod = method.MakeGenericMethod(type);

      return genericMethod.Invoke(null, args);
    }

    private static object ResultOfGenericVersionOfMethod(Type type1, Type type2, string name)
    {
      var method = FindEmptyGenericsMethod(name);

      var genericMethod = method.MakeGenericMethod(type1, type2);

      return genericMethod.Invoke(null, null);
    }

    private static MethodInfo FindEmptyGenericsMethod(string name)
    {
      var methods = typeof(Any).GetMethods(
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        .Where(m => m.IsGenericMethodDefinition)
        .Where(m => !m.GetParameters().Any());
      var method = methods.First(m => m.Name == name);
      return method;
    }


    private static void AssertDynamicEnumConstraintFor<T>()
    {
      if (!typeof(T).IsEnum)
      {
        throw new ArgumentException("T must be an enum type. For other types, use Any.OtherThan()");
      }
    }
  }
}
