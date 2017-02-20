using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkit
{
  public class AllGenerator
  {
    public static AllGenerator CreateAllGenerator()
    {
      return new AllGenerator(new AutoFixtureFactory().CreateCustomAutoFixture(), new Fixture()
      {
        RepeatCount = 0,
      });
    }

    private AllGenerator(Fixture generator, Fixture emptyCollectionGenerator)
    {
      _generator = generator;
      _stringGenerator = new StringGenerator(_generator, this);
      _emptyCollectionGenerator = emptyCollectionGenerator;
      ProxyBasedGenerator = new ProxyBasedGenerator(_emptyCollectionGenerator, this); //bug move this to argument
      NumericGenerator = new NumericGenerator(this);
    }

    public const int Many = 3;
    private readonly ArrayElementPicking _arrayElementPicking = new ArrayElementPicking();

    private readonly CircularList<char> _letters =
      CircularList.CreateStartingFromRandom("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());

    private readonly CircularList<char> _digitChars =
      CircularList.CreateStartingFromRandom("5647382910".ToCharArray());

    private readonly Random _randomGenerator = new Random(System.Guid.NewGuid().GetHashCode());
    private readonly Fixture _emptyCollectionGenerator;
    private readonly StringGenerator _stringGenerator;
    private readonly Fixture _generator;

    private ProxyBasedGenerator ProxyBasedGenerator { get; }

    private NumericGenerator NumericGenerator { get; }

    public IPAddress IpAddress()
    {
      return _generator.Create<IPAddress>();
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      T currentValue;
      do
      {
        currentValue = ValueOf<T>();
      } while (omittedValues.Contains(currentValue));

      return currentValue;
    }

    public T From<T>(params T[] possibleValues)
    {
      var latestArraysWithPossibleValues = _arrayElementPicking.For<T>();

      if (!latestArraysWithPossibleValues.Contain(possibleValues))
      {
        latestArraysWithPossibleValues.Add(possibleValues);
      }

      var result = latestArraysWithPossibleValues.PickNextElementFrom(possibleValues);
      return result;
    }

    public DateTime DateTime()
    {
      return ValueOf<DateTime>();
    }

    public TimeSpan TimeSpan()
    {
      return ValueOf<TimeSpan>();
    }

    public T ValueOf<T>()
    {
      //bug: add support for creating generic structs with interfaces
      return _generator.Create<T>();
    }

    public List<T> EmptyEnumerableOf<T>()
    {
      return _emptyCollectionGenerator.Create<List<T>>();
    }

    internal object Instance(Type type)
    {
      return ProxyBasedGenerator.Instance(type);
    }

    public string LegalXmlTagName()
    {
      return _stringGenerator.LegalXmlTagName();
    }

    public bool Boolean()
    {
      return ValueOf<bool>();
    }

    public object Object()
    {
      return ValueOf<object>();
    }

    public MethodInfo Method()
    {
      return ValueOf<MethodInfo>();
    }

    public Type Type()
    {
      return ValueOf<Type>();
    }

    public T InstanceOf<T>()
    {
      return ProxyBasedGenerator.InstanceOf<T>();
    }

    public T Instance<T>()
    {
      return ProxyBasedGenerator.Instance<T>();
    }

    public T Dummy<T>()
    {
      return ProxyBasedGenerator.Dummy<T>();
    }

    public T SubstituteOf<T>() where T : class
    {
      return ProxyBasedGenerator.SubstituteOf<T>();
    }

    public T OtherThan<T>(params T[] omittedValues)
    {
      return ProxyBasedGenerator.OtherThan(omittedValues);
    }

    public Uri Uri()
    {
      return ValueOf<Uri>();
    }

    public Guid Guid()
    {
      return ValueOf<Guid>();
    }

    public string UrlString()
    {
      return _stringGenerator.UrlString();
    }

    public Exception Exception()
    {
      return ValueOf<Exception>();
    }

    public int Port()
    {
      return _randomGenerator.Next(65535);
    }

    public string Ip()
    {
      return _stringGenerator.Ip();
    }

    public object ValueOf(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public object EmptyEnumerableOf(Type collectionType)
    {
      return ResultOfGenericVersionOfMethod(
        collectionType, MethodBase.GetCurrentMethod().Name);
    }

    public object InstanceOtherThanObjects(Type type, params object[] omittedValues)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name, omittedValues);
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return ProxyBasedGenerator.InstanceOtherThanObjects<T>(omittedValues);
    }


    public IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      var list = new List<T>();
      list.Add(Instance<T>());
      list.AddRange(included);
      list.Add(Instance<T>());

      return list;
    }

    public Task NotStartedTask()
    {
      return new Task(() => Task.Delay(1).Wait());
    }

    public Task<T> NotStartedTask<T>()
    {
      return new Task<T>(Instance<T>);
    }

    public Task StartedTask()
    {
      return Clone.Of(Task.Delay(0));
    }

    public Task<T> StartedTask<T>()
    {
      return Task.FromResult(Instance<T>());
    }

    public Func<T> Func<T>()
    {
      return Instance<Func<T>>();
    }

    public Func<T1, T2> Func<T1, T2>()
    {
      return Instance<Func<T1, T2>>();
    }

    public Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return Instance<Func<T1, T2, T3>>();
    }

    public Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return Instance<Func<T1, T2, T3, T4>>();
    }

    public Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return Instance<Func<T1, T2, T3, T4, T5>>();
    }

    public Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return Instance<Func<T1, T2, T3, T4, T5, T6>>();
    }

    public Action Action()
    {
      return Instance<Action>();
    }

    public Action<T> Action<T>()
    {
      return Instance<Action<T>>();
    }

    public Action<T1, T2> Action<T1, T2>()
    {
      return Instance<Action<T1, T2>>();
    }

    public Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return Instance<Action<T1, T2, T3>>();
    }

    public Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return Instance<Action<T1, T2, T3, T4>>();
    }

    public Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return Instance<Action<T1, T2, T3, T4, T5>>();
    }

    public Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return Instance<Action<T1, T2, T3, T4, T5, T6>>();
    }

    public T Of<T>() where T : struct, IConvertible
    {
      AssertDynamicEnumConstraintFor<T>();
      return ValueOf<T>();
    }

    /// <typeparam name="T">MUST BE AN ENUM. FOR NORMAL VALUES, USE AllGenerator.OtherThan()</typeparam>
    /// <param name="excludedValues"></param>
    /// <returns></returns>
    public T Besides<[MustBeAnEnum] T>([MustBeAnEnum] params T[] excludedValues) where T : struct, IConvertible
    {
      AssertDynamicEnumConstraintFor<T>();
      return ValueOtherThan(excludedValues);
    }

    public IEnumerable<T> Enumerable<T>()
    {
      return Enumerable<T>(length: Many);
    }

    public IEnumerable<T> Enumerable<T>(int length)
    {
      return AddManyTo(new List<T>(), length);
    }

    public IEnumerable<T> EnumerableWithout<T>(params T[] excluded)
    {
      var result = new List<T>
      {
        OtherThan(excluded), 
        OtherThan(excluded), 
        OtherThan(excluded)
      };
      return result;
    }

    public T[] Array<T>()
    {
      return Array<T>(Many);
    }

    public T[] Array<T>(int length)
    {
      return Enumerable<T>(length).ToArray();
    }

    public T[] ArrayWithout<T>(params T[] excluded)
    {
      return EnumerableWithout(excluded).ToArray();
    }

    public T[] ArrayWith<T>(params T[] included)
    {
      return EnumerableWith(included).ToArray();
    }

    public T[] ArrayWithout<T>(IEnumerable<T> excluded)
    {
      return EnumerableWithout(excluded.ToArray()).ToArray();
    }

    public T[] ArrayWith<T>(IEnumerable<T> included)
    {
      return EnumerableWith(included.ToArray()).ToArray();
    }

    public List<T> List<T>()
    {
      return List<T>(Many);
    }

    public List<T> List<T>(int length)
    {
      return Enumerable<T>(length).ToList();
    }

    public List<T> ListWithout<T>(params T[] excluded)
    {
      return EnumerableWithout(excluded).ToList();
    }

    public List<T> ListWith<T>(params T[] included)
    {
      return EnumerableWith(included).ToList();
    }

    public List<T> ListWithout<T>(IEnumerable<T> excluded)
    {
      return EnumerableWithout(excluded.ToArray()).ToList();
    }

    public List<T> ListWith<T>(IEnumerable<T> included)
    {
      return EnumerableWith(included.ToArray()).ToList();
    }

    public IReadOnlyList<T> ReadOnlyList<T>()
    {
      return ReadOnlyList<T>(Many);
    }

    public IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return List<T>(length);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return ListWith(items);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return ListWith(items);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return ListWithout(items);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return ListWithout(items);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>()
    {
      return SortedList<TKey, TValue>(Many);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>(int length)
    {
      var list = new SortedList<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        list.Add(Instance<TKey>(), Instance<TValue>());
      }
      return list;
    }

    public ISet<T> Set<T>(int length)
    {
      return new HashSet<T>(Enumerable<T>(length));
    }

    public ISet<T> Set<T>()
    {
      return Set<T>(Many);
    }

    public ISet<T> SortedSet<T>(int length)
    {
      return new SortedSet<T>(Enumerable<T>(length));
    }

    public ISet<T> SortedSet<T>()
    {
      return SortedSet<T>(Many);
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length)
    {
      var dict = new Dictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.Add(Instance<TKey>(), Instance<TValue>());
      }
      return dict;
    }

    public Dictionary<T, U> DictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      var dict = Dictionary<T, U>(0);
      foreach (var key in keys)
      {
        dict.Add(key, InstanceOf<U>());
      }

      return dict;
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return Dictionary<TKey, TValue>(Many);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return Dictionary<TKey, TValue>(length);
    }

    public IReadOnlyDictionary<T, U> ReadOnlyDictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      return DictionaryWithKeys<T, U>(keys);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>()
    {
      return ReadOnlyDictionary<TKey, TValue>(Many);
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length)
    {
      var dict = new SortedDictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.Add(Instance<TKey>(), Instance<TValue>());
      }
      return dict;
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>()
    {
      return SortedDictionary<TKey, TValue>(Many);
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length)
    {
      var dict = new ConcurrentDictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.TryAdd(Instance<TKey>(), Instance<TValue>());
      }
      return dict;

    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>()
    {
      return ConcurrentDictionary<TKey, TValue>(Many);
    }

    public ConcurrentStack<T> ConcurrentStack<T>()
    {
      return ConcurrentStack<T>(Many);
    }

    public ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      var coll = new ConcurrentStack<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Push(Instance<T>());
      }
      return coll;
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return ConcurrentQueue<T>(Many);
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      var coll = new ConcurrentQueue<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Enqueue(Instance<T>());
      }
      return coll;

    }

    public ConcurrentBag<T> ConcurrentBag<T>()
    {
      return ConcurrentBag<T>(Many);
    }

    public ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      var coll = new ConcurrentBag<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Add(Instance<T>());
      }
      return coll;

    }

    public IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return SortedSet<T>(length).ToList();
    }

    public IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return EnumerableSortedDescending<T>(Many);
    }

    public IEnumerator<T> Enumerator<T>()
    {
      return List<T>().GetEnumerator();
    }

    public object List(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public object Set(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public object Dictionary(Type keyType, Type valueType)
    {
      return ResultOfGenericVersionOfMethod(keyType, valueType, MethodBase.GetCurrentMethod().Name);
    }

    public object SortedList(Type keyType, Type valueType)
    {
      return ResultOfGenericVersionOfMethod(keyType, valueType, MethodBase.GetCurrentMethod().Name);
    }

    public object SortedSet(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name); 
    }

    public object SortedDictionary(Type keyType, Type valueType)
    {
      return ResultOfGenericVersionOfMethod(keyType, valueType, MethodBase.GetCurrentMethod().Name);
    }

    public object ConcurrentDictionary(Type keyType, Type valueType)
    {
      return ResultOfGenericVersionOfMethod(keyType, valueType, MethodBase.GetCurrentMethod().Name);
    }

    public object Array(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name); 
    }

    public ICollection<T> AddManyTo<T>(ICollection<T> collection, int many)
    {
      for (int i = 0; i < many; ++i)
      {
        collection.Add(Instance<T>());
      }
      return collection;
    }

    public object KeyValuePair(Type keyType, Type valueType)
    {
      return Activator.CreateInstance(
        typeof (KeyValuePair<,>).MakeGenericType(keyType, valueType), ProxyBasedGenerator.Instance(keyType), ProxyBasedGenerator.Instance(valueType)
      );
    }

    public object Enumerator(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name); 
    }

    public object ConcurrentStack(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public object ConcurrentQueue(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public object ConcurrentBag(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public string String()
    {
      return _stringGenerator.String();
    }

    public string String(string seed)
    {
      return _stringGenerator.String(seed);
    }

    public string LowerCaseString()
    {
      return _stringGenerator.LowerCaseString();
    }

    public string UpperCaseString()
    {
      return _stringGenerator.UpperCaseString();
    }

    public string LowerCaseAlphaString()
    {
      return _stringGenerator.LowerCaseAlphaString();
    }

    public string UpperCaseAlphaString()
    {
      return _stringGenerator.UpperCaseAlphaString();
    }

    public string StringMatching(string pattern)
    {
      return _stringGenerator.StringMatching(pattern);
    }

    public string StringOfLength(int charactersCount)
    {
      return _stringGenerator.StringOfLength(charactersCount);
    }

    public string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return _stringGenerator.StringOtherThan(alreadyUsedStrings);
    }

    public string StringNotContaining<T>(params T[] excludedObjects)
    {
      return _stringGenerator.StringNotContaining(excludedObjects);
    }

    public string StringNotContaining(params string[] excludedSubstrings)
    {
      return _stringGenerator.StringNotContaining(excludedSubstrings);
    }

    public string StringContaining<T>(T obj)
    {
      return _stringGenerator.StringContaining(obj);
    }

    public string StringContaining(string str)
    {
      return _stringGenerator.StringContaining(str);
    }

    public string AlphaString()
    {
      return _stringGenerator.AlphaString();
    }

    public string AlphaString(int maxLength)
    {
      return _stringGenerator.AlphaString(maxLength);
    }

    public string Identifier()
    {
      return _stringGenerator.Identifier();
    }

    public char AlphaChar() => 
      _letters.Next();

    public char DigitChar() => 
      _digitChars.Next();

    public char Char() => 
      Instance<char>();

    public string NumericString(int digitsCount = AllGenerator.Many)
    {
      return _stringGenerator.NumericString(digitsCount);
    }

    public char LowerCaseAlphaChar() => char.ToLower(AlphaChar());
    public char UpperCaseAlphaChar() => char.ToUpper(AlphaChar());
    public byte Digit()
    {
      return NumericGenerator.Digit();
    }

    public int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      return NumericGenerator.IntegerFromSequence(startingValue, step);
    }

    public byte Octet()
    {
      return NumericGenerator.Octet();
    }

    public int IntegerDivisibleBy(int quotient)
    {
      return NumericGenerator.IntegerDivisibleBy(quotient);
    }

    public int IntegerNotDivisibleBy(int quotient)
    {
      return NumericGenerator.IntegerNotDivisibleBy(quotient);
    }

    public int IntegerWithExactDigitsCount(int digitsCount)
    {
      return NumericGenerator.IntegerWithExactDigitsCount(digitsCount);
    }

    public long LongIntegerWithExactDigitsCount(int digitsCount)
    {
      return NumericGenerator.LongIntegerWithExactDigitsCount(digitsCount);
    }

    public uint UnsignedIntegerWithExactDigitsCount(int digitsCount)
    {
      return NumericGenerator.UnsignedIntegerWithExactDigitsCount(digitsCount);
    }

    public ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount)
    {
      return NumericGenerator.UnsignedLongIntegerWithExactDigitsCount(digitsCount);
    }

    public byte PositiveDigit()
    {
      return NumericGenerator.PositiveDigit();
    }

    private static object ResultOfGenericVersionOfMethod(Type type, string name)
    {
      return ProxyBasedGenerator.ResultOfGenericVersionOfMethod<Any>(type, name);
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
        throw new ArgumentException("T must be an enum type. For other types, use AllGenerator.OtherThan()");
      }
    }

    public T Exploding<T>() where T : class
    {
      return ProxyBasedGenerator.Exploding<T>();
    }

    public int Integer()
    {
      return NumericGenerator.Integer();
    }

    public double Double()
    {
      return NumericGenerator.Double();
    }

    public double DoubleOtherThan(double[] others)
    {
      return NumericGenerator.DoubleOtherThan(others);
    }

    public long LongInteger()
    {
      return NumericGenerator.LongInteger();
    }

    public long LongIntegerOtherThan(long[] others)
    {
      return NumericGenerator.LongIntegerOtherThan(others);
    }

    public ulong UnsignedLongInteger()
    {
      return NumericGenerator.UnsignedLongInteger();
    }

    public ulong UnsignedLongIntegerOtherThan(ulong[] others)
    {
      return NumericGenerator.UnsignedLongIntegerOtherThan(others);
    }

    public int IntegerOtherThan(int[] others)
    {
      return NumericGenerator.IntegerOtherThan(others);
    }

    public byte Byte()
    {
      return NumericGenerator.Byte();
    }

    public byte ByteOtherThan(byte[] others)
    {
      return NumericGenerator.ByteOtherThan(others);
    }

    public decimal Decimal()
    {
      return NumericGenerator.Decimal();
    }

    public decimal DecimalOtherThan(decimal[] others)
    {
      return NumericGenerator.DecimalOtherThan(others);
    }

    public uint UnsignedInt()
    {
      return NumericGenerator.UnsignedInt();
    }

    public uint UnsignedIntOtherThan(uint[] others)
    {
      return NumericGenerator.UnsignedIntOtherThan(others);
    }

    public ushort UnsignedShort()
    {
      return NumericGenerator.UnsignedShort();
    }

    public ushort UnsignedShortOtherThan(ushort[] others)
    {
      return NumericGenerator.UnsignedShortOtherThan(others);
    }

    public short ShortInteger()
    {
      return NumericGenerator.ShortInteger();
    }

    public short ShortIntegerOtherThan(short[] others)
    {
      return NumericGenerator.ShortIntegerOtherThan(others);
    }
  }
}