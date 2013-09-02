using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;

namespace TddEbook.TddToolkit
{
    public static partial class Any
    {
      private static readonly Fixture _generator = new Fixture();
      private static readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
      private static readonly RegularExpressionGenerator _regexGenerator = new RegularExpressionGenerator();
      private static readonly CachedGeneration _cachedGeneration = new CachedGeneration(new ReturnValueCache());
      private static readonly CircularList<char> _letters = new CircularList<char>("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());
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

      private static readonly NestingLimit _nestingLimit = NestingLimit.Of(5);

      private static object ResultOfGenericVersionOfMethod(Type type, string name)
      {
        return typeof(Any).GetMethods().Where(m => m.Name == name).First(m => m.GetParameters().Length == 0).
          MakeGenericMethod(new[] { type }).Invoke(null, null);
      }

      private static void AssertDynamicEnumConstraintFor<T>()
      {
        if (!typeof(T).IsEnum)
        {
          throw new ArgumentException("T must be an enumerated type");
        }
      }
    }

  internal class NestingLimit
  {
    private readonly int _limit;
    private int _nesting = 0;

    private NestingLimit(int limit)
    {
      _limit = limit;
    }

    public static NestingLimit Of(int limit)
    {
      return new NestingLimit(limit);
    }

    public void AddNesting()
    {
      _nesting++;
    }

    public bool IsReached()
    {
      return _nesting > _limit;
    }

    public void RemoveNesting()
    {
      _nesting--;
    }
  }
}
