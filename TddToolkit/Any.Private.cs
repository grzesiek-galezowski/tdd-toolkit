using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.AutoFixture;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;

namespace TddEbook.TddToolkit
{
    public static partial class Any
    {
      private static readonly Fixture Generator = new Fixture();
      private static readonly Random RandomGenerator = new Random(Guid.NewGuid().GetHashCode());
      private static readonly RegularExpressionGenerator RegexGenerator = new RegularExpressionGenerator();
      private static readonly CachedGeneration CachedGeneration = new CachedGeneration(new ReturnValueCache());
      private static readonly CircularList<char> Letters = new CircularList<char>("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());
      private static readonly CircularList<char> Digits = new CircularList<char>("5647382910".ToCharArray());
      private static readonly CircularList<Type> Types = new CircularList<Type>(
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

      private static readonly CircularList<MethodInfo> MethodList = new CircularList<MethodInfo>(typeof(List<int>).GetMethods(BindingFlags.Public | BindingFlags.Instance));
      private static readonly NestingLimit NestingLimit = NestingLimit.Of(5);

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
}
