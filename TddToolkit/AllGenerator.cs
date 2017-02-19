using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using CommonTypes;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using TddEbook.TddToolkit.ImplementationDetails;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkit
{
  public class AllGenerator
  {
    public AllGenerator()
    {
      _emptyCollectionGenerator = new Fixture()
      {
        RepeatCount = 0,
      };
      _proxyBasedGenerator = new ProxyBasedGenerator(_emptyCollectionGenerator, this);
      _generator = new AutoFixtureFactory().CreateCustomAutoFixture();
    }

    public const int Many = 3;
    private readonly ArrayElementPicking _arrayElementPicking = new ArrayElementPicking();
    private readonly Fixture _generator;

    private readonly Random _randomGenerator = new Random(System.Guid.NewGuid().GetHashCode());
    private readonly RegularExpressionGenerator _regexGenerator = new RegularExpressionGenerator();

    private readonly CircularList<char> _letters =
      CircularList.CreateStartingFromRandom("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());

    private readonly CircularList<char> _digitChars =
      CircularList.CreateStartingFromRandom("5647382910".ToCharArray());

    private readonly CircularList<byte> _digits =
      CircularList.CreateStartingFromRandom(new byte[] {5, 6, 4, 7, 3, 8, 2, 9, 1, 0});


    private readonly HashSet<IntegerSequence> _sequences = new HashSet<IntegerSequence>();

    private readonly CircularList<int> _numbersToMultiply = CircularList.CreateStartingFromRandom(
      System.Linq.Enumerable.Range(1, 100).ToArray());

    private readonly NumericTraits<int> _intTraits = NumericTraits.Integer();
    private readonly NumericTraits<long> _longTraits = NumericTraits.Long();
    private readonly NumericTraits<uint> _uintTraits = NumericTraits.UnsignedInteger();
    private readonly NumericTraits<ulong> _ulongTraits = NumericTraits.UnsignedLong();
    private readonly ProxyBasedGenerator _proxyBasedGenerator;
    private readonly Fixture _emptyCollectionGenerator;

    public ProxyBasedGenerator ProxyBasedGenerator
    {
      get { return _proxyBasedGenerator; }
    }

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

    public string LegalXmlTagName()
    {
      return Identifier();
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
      return _proxyBasedGenerator.InstanceOf<T>();
    }

    public T Instance<T>()
    {
      return _proxyBasedGenerator.Instance<T>();
    }

    public T Dummy<T>()
    {
      return _proxyBasedGenerator.Dummy<T>();
    }

    public T SubstituteOf<T>() where T : class
    {
      return _proxyBasedGenerator.SubstituteOf<T>();
    }

    public T OtherThan<T>(params T[] omittedValues)
    {
      return _proxyBasedGenerator.OtherThan(omittedValues);
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
      return Uri().ToString();
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
      return _randomGenerator.Next(256) + "."
             + _randomGenerator.Next(256) + "."
             + _randomGenerator.Next(256) + "."
             + _randomGenerator.Next(256);
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

    public string String() => _generator.Create<string>();
    public string String(string seed) => _generator.Create(seed+"_");
    public string LowerCaseString() => String().ToLower();
    public string UpperCaseString() => String().ToUpper();
    public string LowerCaseAlphaString() => AlphaString().ToLower();
    public string UpperCaseAlphaString() => AlphaString().ToUpper();

    public string StringMatching(string pattern)
    {
      var request = new RegularExpressionRequest(pattern);

      var result = _regexGenerator.Create(request, new DummyContext());
      return result.ToString();
    }

    public string StringOfLength(int charactersCount)
    {
      var result = string.Empty;
      while (result.Count() < charactersCount)
      {
        result += String();
      }
      return result.Substring(0, charactersCount);
    }

    public string StringOtherThan(params string[] alreadyUsedStrings) => 
      ValueOtherThan(alreadyUsedStrings);

    public string StringNotContaining<T>(params T[] excludedObjects) => 
      StringNotContaining((from obj in excludedObjects select obj.ToString()).ToArray());

    public string StringNotContaining(params string[] excludedSubstrings)
    {
      var preprocessedStrings = from str in excludedSubstrings
        where !string.IsNullOrEmpty(str)
        select str;

      string result = String();
      bool found = false;
      for(int i = 0 ; i < 100 ; ++i)
      {
        result = String();
        if (preprocessedStrings.Any(result.Contains))
        {
          found = true;
          break;
        }
      }
      if (!found)
      {
        foreach (var excludedSubstring in excludedSubstrings.Where(s => s != string.Empty))
        {
          result = result.Replace(excludedSubstring, "");
        }
      }
      return result;
    }

    public string StringContaining<T>(T obj) => 
      StringContaining(obj.ToString());

    public string StringContaining(string str) => 
      String() + str + String();

    public string AlphaString() => 
      AlphaString(String().Length);

    public string AlphaString(int maxLength)
    {
      var result = System.String.Empty;
      for (var i = 0; i < maxLength; ++i)
      {
        result += AlphaChar();
      }
      return result;
    }

    public string Identifier()
    {
      string result = AlphaChar().ToString(CultureInfo.InvariantCulture);
      for (var i = 0; i < 5; ++i)
      {
        result += DigitChar();
        result += AlphaChar();
      }
      return result;
    }

    public char AlphaChar() => 
      _letters.Next();

    public char DigitChar() => 
      _digitChars.Next();

    public char Char() => 
      Instance<char>();

    public string NumericString(int digitsCount = Many) => 
      StringMatching("[1-9][0-9]{" + (digitsCount - 1) + "}");

    public char LowerCaseAlphaChar() => char.ToLower(AlphaChar());
    public char UpperCaseAlphaChar() => char.ToUpper(AlphaChar());
    public int Integer() => ValueOf<int>();
    public double Double() => ValueOf<double>();
    public double DoubleOtherThan(params double[] others) => ValueOtherThan(others);
    public long LongInteger() => ValueOf<long>();
    public long LongIntegerOtherThan(params long[] others) => ValueOtherThan(others);
    public ulong UnsignedLongInteger() => ValueOf<ulong>();
    public ulong UnsignedLongIntegerOtherThan(params ulong[] others) => ValueOtherThan(others);
    public int IntegerOtherThan(params int[] others) => ValueOtherThan(others);
    public byte Byte() => ValueOf<byte>();
    public byte ByteOtherThan(params byte[] others) => ValueOtherThan(others);
    public decimal Decimal() => ValueOf<decimal>();
    public decimal DecimalOtherThan(params decimal[] others) => ValueOtherThan(others);
    public uint UnsignedInt() => ValueOf<uint>();
    public uint UnsignedIntOtherThan(params uint[] others) => ValueOtherThan(others);
    public ushort UnsignedShort() => ValueOf<ushort>();
    public ushort UnsignedShortOtherThan(params ushort[] others) => ValueOtherThan(others);
    public short ShortInteger() => ValueOf<short>();
    public short ShortIntegerOtherThan(params short[] others) => ValueOtherThan(others);
    public byte Digit() => _digits.Next();

    public int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      var sequence = new IntegerSequence(startingValue, step);
      var finalSequence = Maybe.Wrap(_sequences.FirstOrDefault(s => s.Equals(sequence))).ValueOr(sequence);
      _sequences.Add(finalSequence);
      var integerFromSequence = finalSequence.Next();
      return integerFromSequence;
    }

    public byte Octet()
    {
      return Byte();
    }

    public int IntegerDivisibleBy(int quotient)
    {
      return _numbersToMultiply.Next() * quotient;
    }

    public int IntegerNotDivisibleBy(int quotient)
    {
      AssertQuotientMakesSense(quotient);
      return IntegerDivisibleBy(quotient) + 1;
    }

    public void AssertQuotientMakesSense(int quotient)
    {
      if (quotient == 1 || quotient == -1 || quotient == 0)
      {
        throw new ArgumentException($"generating an integer not dividable by {quotient} is not supported");
      }
    }

    public int IntegerWithExactDigitsCount(int digitsCount) => 
      _intTraits.GenerateWithExactNumberOfDigits(digitsCount, _randomGenerator);

    public long LongIntegerWithExactDigitsCount(int digitsCount) => 
      _longTraits.GenerateWithExactNumberOfDigits(digitsCount, _randomGenerator);

    public uint UnsignedIntegerWithExactDigitsCount(int digitsCount) => 
      _uintTraits.GenerateWithExactNumberOfDigits(digitsCount, _randomGenerator);

    public ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount) => 
      _ulongTraits.GenerateWithExactNumberOfDigits(digitsCount, _randomGenerator);

    public byte PositiveDigit()
    {
      byte digit = Digit();
      while (digit == 0)
      {
        digit = Digit();
      }
      return digit;
    }

    private static object ResultOfGenericVersionOfMethod(Type type, string name)
    {
      return ProxyBasedGenerator.ResultOfGenericVersionOfMethod(type, name);
    }

    private static Func<MethodInfo, bool> ParameterlessGenericVersion()
    {
      return ProxyBasedGenerator.ParameterlessGenericVersion();
    }

    private static Func<MethodInfo, bool> NameIs(string name)
    {
      return ProxyBasedGenerator.NameIs(name);
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
  }
}