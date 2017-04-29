using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class EmptyCollectionGenerator
  {
    public readonly Fixture _emptyCollectionGenerator;
    private readonly GenericMethodProxyCalls _genericMethodProxyCalls;

    public EmptyCollectionGenerator(Fixture emptyCollectionGenerator, GenericMethodProxyCalls genericMethodProxyCalls)
    {
      _emptyCollectionGenerator = emptyCollectionGenerator;
      _genericMethodProxyCalls = genericMethodProxyCalls;
    }

    public List<T> EmptyEnumerableOf<T>()
    {
      return _emptyCollectionGenerator.Create<List<T>>();
    }

    public object EmptyEnumerableOf(Type collectionType)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod<Any>(
        collectionType, MethodBase.GetCurrentMethod().Name);
    }
  }

  public class CharGenerator
  {
    private readonly CircularList<char> _letters =
      CircularList.CreateStartingFromRandom("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());

    private readonly CircularList<char> _digitChars =
      CircularList.CreateStartingFromRandom("5647382910".ToCharArray());

    private readonly ValueGenerator _valueGenerator;

    public CharGenerator(ValueGenerator valueGenerator)
    {
      _valueGenerator = valueGenerator;
    }

    public char AlphaChar() => 
      _letters.Next();

    public char DigitChar() => 
      _digitChars.Next();

    public char Char() => _valueGenerator.ValueOf<char>();
    public char LowerCaseAlphaChar() => char.ToLower(AlphaChar());
    public char UpperCaseAlphaChar() => char.ToUpper(AlphaChar());
  }

  public class AllGenerator : IProxyBasedGenerator
  {
    public static AllGenerator CreateAllGenerator()
    {
      return new AllGenerator(
        new AutoFixtureFactory(), new Fixture()
      {
        RepeatCount = 0,
      });
    }

    private AllGenerator(AutoFixtureFactory fixtureFactory, Fixture emptyCollectionGenerator)
    {
      var genericMethodProxyCalls = new GenericMethodProxyCalls();
      var customAutoFixture = fixtureFactory.CreateCustomAutoFixture(this);
      _emptyCollectionGenerator = new EmptyCollectionGenerator(emptyCollectionGenerator, genericMethodProxyCalls);
      ProxyBasedGenerator = new ProxyBasedGenerator(emptyCollectionGenerator, this, genericMethodProxyCalls); //bug move this to argument
      _valueGenerator = new ValueGenerator(customAutoFixture, ProxyBasedGenerator);
      _charGenerator = new CharGenerator(_valueGenerator);
      _stringGenerator = new StringGenerator(customAutoFixture, this, this._charGenerator);
      NumericGenerator = new NumericGenerator(_valueGenerator);
      _collectionGenerator = new CollectionGenerator(ProxyBasedGenerator);
    }

    public const int Many = 3;
    private readonly ArrayElementPicking _arrayElementPicking = new ArrayElementPicking();

    private readonly Random _randomGenerator = new Random(System.Guid.NewGuid().GetHashCode());
    private readonly StringGenerator _stringGenerator;
    private readonly CollectionGenerator _collectionGenerator;
    private readonly ValueGenerator _valueGenerator;
    private readonly EmptyCollectionGenerator _emptyCollectionGenerator;
    private readonly CharGenerator _charGenerator;

    private ProxyBasedGenerator ProxyBasedGenerator { get; }

    private NumericGenerator NumericGenerator { get; }

    public IPAddress IpAddress()
    {
      return _valueGenerator.ValueOf<IPAddress>();
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      return _valueGenerator.ValueOtherThan(omittedValues);
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
      return _valueGenerator.ValueOf<T>();
    }

    public List<T> EmptyEnumerableOf<T>()
    {
      return _emptyCollectionGenerator.EmptyEnumerableOf<T>();
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
      return _valueGenerator.ValueOf(type);
    }

    public object EmptyEnumerableOf(Type collectionType)
    {
      return _emptyCollectionGenerator.EmptyEnumerableOf(collectionType);
    }

    public object InstanceOtherThanObjects(Type type, params object[] omittedValues)
    {
      return ProxyBasedGenerator.ResultOfGenericVersionOfMethod<Any>(type, MethodBase.GetCurrentMethod().Name, omittedValues);
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return ProxyBasedGenerator.InstanceOtherThanObjects<T>(omittedValues);
    }


    public IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      return _collectionGenerator.EnumerableWith(included);
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
      return _collectionGenerator.Enumerable<T>();
    }

    public IEnumerable<T> Enumerable<T>(int length)
    {
      return _collectionGenerator.Enumerable<T>(length);
    }

    public IEnumerable<T> EnumerableWithout<T>(params T[] excluded)
    {
      return _collectionGenerator.EnumerableWithout(excluded);
    }

    public T[] Array<T>()
    {
      return _collectionGenerator.Array<T>();
    }

    public T[] Array<T>(int length)
    {
      return _collectionGenerator.Array<T>(length);
    }

    public T[] ArrayWithout<T>(params T[] excluded)
    {
      return _collectionGenerator.ArrayWithout(excluded);
    }

    public T[] ArrayWith<T>(params T[] included)
    {
      return _collectionGenerator.ArrayWith(included);
    }

    public T[] ArrayWithout<T>(IEnumerable<T> excluded)
    {
      return _collectionGenerator.ArrayWithout(excluded);
    }

    public T[] ArrayWith<T>(IEnumerable<T> included)
    {
      return _collectionGenerator.ArrayWith(included);
    }

    public List<T> List<T>()
    {
      return _collectionGenerator.List<T>();
    }

    public List<T> List<T>(int length)
    {
      return _collectionGenerator.List<T>(length);
    }

    public List<T> ListWithout<T>(params T[] excluded)
    {
      return _collectionGenerator.ListWithout(excluded);
    }

    public List<T> ListWith<T>(params T[] included)
    {
      return _collectionGenerator.ListWith(included);
    }

    public List<T> ListWithout<T>(IEnumerable<T> excluded)
    {
      return _collectionGenerator.ListWithout(excluded);
    }

    public List<T> ListWith<T>(IEnumerable<T> included)
    {
      return _collectionGenerator.ListWith(included);
    }

    public IReadOnlyList<T> ReadOnlyList<T>()
    {
      return _collectionGenerator.ReadOnlyList<T>();
    }

    public IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return _collectionGenerator.ReadOnlyList<T>(length);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return _collectionGenerator.ReadOnlyListWith(items);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return _collectionGenerator.ReadOnlyListWith(items);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return _collectionGenerator.ReadOnlyListWithout(items);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return _collectionGenerator.ReadOnlyListWithout(items);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>()
    {
      return _collectionGenerator.SortedList<TKey, TValue>();
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>(int length)
    {
      return _collectionGenerator.SortedList<TKey, TValue>(length);
    }

    public ISet<T> Set<T>(int length)
    {
      return _collectionGenerator.Set<T>(length);
    }

    public ISet<T> Set<T>()
    {
      return _collectionGenerator.Set<T>();
    }

    public ISet<T> SortedSet<T>(int length)
    {
      return _collectionGenerator.SortedSet<T>(length);
    }

    public ISet<T> SortedSet<T>()
    {
      return _collectionGenerator.SortedSet<T>();
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length)
    {
      return _collectionGenerator.Dictionary<TKey, TValue>(length);
    }

    public Dictionary<T, U> DictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      return _collectionGenerator.DictionaryWithKeys<T, U>(keys);
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return _collectionGenerator.Dictionary<TKey, TValue>();
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return _collectionGenerator.ReadOnlyDictionary<TKey, TValue>(length);
    }

    public IReadOnlyDictionary<T, U> ReadOnlyDictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      return _collectionGenerator.ReadOnlyDictionaryWithKeys<T, U>(keys);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>()
    {
      return _collectionGenerator.ReadOnlyDictionary<TKey, TValue>();
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length)
    {
      return _collectionGenerator.SortedDictionary<TKey, TValue>(length);
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>()
    {
      return _collectionGenerator.SortedDictionary<TKey, TValue>();
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length)
    {
      return _collectionGenerator.ConcurrentDictionary<TKey, TValue>(length);
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>()
    {
      return _collectionGenerator.ConcurrentDictionary<TKey, TValue>();
    }

    public ConcurrentStack<T> ConcurrentStack<T>()
    {
      return _collectionGenerator.ConcurrentStack<T>();
    }

    public ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      return _collectionGenerator.ConcurrentStack<T>(length);
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return _collectionGenerator.ConcurrentQueue<T>();
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      return _collectionGenerator.ConcurrentQueue<T>(length);
    }

    public ConcurrentBag<T> ConcurrentBag<T>()
    {
      return _collectionGenerator.ConcurrentBag<T>();
    }

    public ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      return _collectionGenerator.ConcurrentBag<T>(length);
    }

    public IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return _collectionGenerator.EnumerableSortedDescending<T>(length);
    }

    public IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return _collectionGenerator.EnumerableSortedDescending<T>();
    }

    public IEnumerator<T> Enumerator<T>()
    {
      return _collectionGenerator.Enumerator<T>();
    }

    public object List(Type type)
    {
      return _collectionGenerator.List(type);
    }

    public object Set(Type type)
    {
      return _collectionGenerator.Set(type);
    }

    public object Dictionary(Type keyType, Type valueType)
    {
      return _collectionGenerator.Dictionary(keyType, valueType);
    }

    public object SortedList(Type keyType, Type valueType)
    {
      return _collectionGenerator.SortedList(keyType, valueType);
    }

    public object SortedSet(Type type)
    {
      return _collectionGenerator.SortedSet(type);
    }

    public object SortedDictionary(Type keyType, Type valueType)
    {
      return _collectionGenerator.SortedDictionary(keyType, valueType);
    }

    public object ConcurrentDictionary(Type keyType, Type valueType)
    {
      return _collectionGenerator.ConcurrentDictionary(keyType, valueType);
    }

    public object Array(Type type)
    {
      return _collectionGenerator.Array(type);
    }

    public ICollection<T> AddManyTo<T>(ICollection<T> collection, int many)
    {
      return _collectionGenerator.AddManyTo(collection, many);
    }

    public object KeyValuePair(Type keyType, Type valueType)
    {
      return Activator.CreateInstance(
        typeof (KeyValuePair<,>).MakeGenericType(keyType, valueType), ProxyBasedGenerator.Instance(keyType), ProxyBasedGenerator.Instance(valueType)
      );
    }

    public object Enumerator(Type type)
    {
      return _collectionGenerator.Enumerator(type);
    }

    public object ConcurrentStack(Type type)
    {
      return _collectionGenerator.ConcurrentStack(type);
    }

    public object ConcurrentQueue(Type type)
    {
      return _collectionGenerator.ConcurrentQueue(type);
    }

    public object ConcurrentBag(Type type)
    {
      return _collectionGenerator.ConcurrentBag(type);
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


    public string NumericString(int digitsCount = AllGenerator.Many)
    {
      return _stringGenerator.NumericString(digitsCount);
    }

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

    public MethodInfo FindEmptyGenericsMethod<T>(string name)
    {
      var methods = typeof(T).GetMethods(
          BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        .Where(m => m.IsGenericMethodDefinition)
        .Where(m => !m.GetParameters().Any());
      var method = methods.First(m => m.Name == name);
      return method;
    }

    public object ResultOfGenericVersionOfMethod<T>(Type type1, Type type2, string name)
    {
      var method = FindEmptyGenericsMethod<T>(name);

      var genericMethod = method.MakeGenericMethod(type1, type2);

      return genericMethod.Invoke(null, null);
    }

    public object ResultOfGenericVersionOfMethod(Type type, string name)
    {
      return ProxyBasedGenerator.ResultOfGenericVersionOfMethod<Any>(type, name);
    }

    public char AlphaChar()
    {
      return _charGenerator.AlphaChar();
    }

    public char DigitChar()
    {
      return _charGenerator.DigitChar();
    }

    public char Char()
    {
      return _charGenerator.Char();
    }

    public char LowerCaseAlphaChar()
    {
      return _charGenerator.LowerCaseAlphaChar();
    }

    public char UpperCaseAlphaChar()
    {
      return _charGenerator.UpperCaseAlphaChar();
    }
  }
}