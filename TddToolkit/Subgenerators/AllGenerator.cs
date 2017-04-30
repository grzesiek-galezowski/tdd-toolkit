using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class InvokableGenerator
  {
    private readonly IProxyBasedGenerator _proxyBasedGenerator;

    public InvokableGenerator(IProxyBasedGenerator proxyBasedGenerator)
    {
      _proxyBasedGenerator = proxyBasedGenerator;
    }

    public Task NotStartedTask()
    {
      return new Task(() => Task.Delay(1).Wait());
    }

    public Task<T> NotStartedTask<T>()
    {
      return new Task<T>(_proxyBasedGenerator.Instance<T>);
    }

    public Task StartedTask()
    {
      return Clone.Of(Task.Delay(0));
    }

    public Task<T> StartedTask<T>()
    {
      return Task.FromResult(_proxyBasedGenerator.Instance<T>());
    }

    public Func<T> Func<T>()
    {
      return _proxyBasedGenerator.Instance<Func<T>>();
    }

    public Func<T1, T2> Func<T1, T2>()
    {
      return _proxyBasedGenerator.Instance<Func<T1, T2>>();
    }

    public Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return _proxyBasedGenerator.Instance<Func<T1, T2, T3>>();
    }

    public Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return _proxyBasedGenerator.Instance<Func<T1, T2, T3, T4>>();
    }

    public Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return _proxyBasedGenerator.Instance<Func<T1, T2, T3, T4, T5>>();
    }

    public Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return _proxyBasedGenerator.Instance<Func<T1, T2, T3, T4, T5, T6>>();
    }

    public Action Action()
    {
      return _proxyBasedGenerator.Instance<Action>();
    }

    public Action<T> Action<T>()
    {
      return _proxyBasedGenerator.Instance<Action<T>>();
    }

    public Action<T1, T2> Action<T1, T2>()
    {
      return _proxyBasedGenerator.Instance<Action<T1, T2>>();
    }

    public Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return _proxyBasedGenerator.Instance<Action<T1, T2, T3>>();
    }

    public Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return _proxyBasedGenerator.Instance<Action<T1, T2, T3, T4>>();
    }

    public Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return _proxyBasedGenerator.Instance<Action<T1, T2, T3, T4, T5>>();
    }

    public Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return _proxyBasedGenerator.Instance<Action<T1, T2, T3, T4, T5, T6>>();
    }
  }

  public class AllGenerator : IProxyBasedGenerator
  {
    public AllGenerator(
      ValueGenerator valueGenerator, 
      CharGenerator charGenerator, 
      SpecificTypeObjectGenerator specificTypeObjectGenerator, 
      StringGenerator stringGenerator, 
      EmptyCollectionGenerator emptyCollectionGenerator, 
      NumericGenerator numericGenerator, 
      ProxyBasedGenerator proxyBasedGenerator, 
      CollectionGenerator collectionGenerator, 
      InvokableGenerator invokableGenerator)
    {
      _valueGenerator = valueGenerator;
      _charGenerator = charGenerator;
      _specificTypeObjectGenerator = specificTypeObjectGenerator;
      _stringGenerator = stringGenerator;
      _emptyCollectionGenerator = emptyCollectionGenerator;
      _numericGenerator = numericGenerator;
      _proxyBasedGenerator = proxyBasedGenerator;
      _collectionGenerator = collectionGenerator;
      _invokableGenerator = invokableGenerator;
    }

    public const int Many = 3;
    private readonly ArrayElementPicking _arrayElementPicking = new ArrayElementPicking();

    private readonly Random _randomGenerator = new Random(System.Guid.NewGuid().GetHashCode());
    private readonly StringGenerator _stringGenerator;
    private readonly CollectionGenerator _collectionGenerator;
    private readonly ValueGenerator _valueGenerator;
    private readonly EmptyCollectionGenerator _emptyCollectionGenerator;
    private readonly CharGenerator _charGenerator;
    private readonly SpecificTypeObjectGenerator _specificTypeObjectGenerator;
    private readonly ProxyBasedGenerator _proxyBasedGenerator;
    private readonly NumericGenerator _numericGenerator;
    private readonly InvokableGenerator _invokableGenerator;

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

    public IEnumerable<T> EmptyEnumerableOf<T>()
    {
      return _emptyCollectionGenerator.EmptyEnumerableOf<T>();
    }

    internal object Instance(Type type)
    {
      return _proxyBasedGenerator.Instance(type);
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
      return _specificTypeObjectGenerator.Uri();
    }

    public Guid Guid()
    {
      return _specificTypeObjectGenerator.Guid();
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
      return _proxyBasedGenerator.ResultOfGenericVersionOfMethod<Any>(type, MethodBase.GetCurrentMethod().Name, omittedValues);
    }

    public T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return _proxyBasedGenerator.InstanceOtherThanObjects<T>(omittedValues);
    }

    public IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      return _collectionGenerator.EnumerableWith(included);
    }

    public Task NotStartedTask()
    {
      return _invokableGenerator.NotStartedTask();
    }

    public Task<T> NotStartedTask<T>()
    {
      return _invokableGenerator.NotStartedTask<T>();
    }

    public Task StartedTask()
    {
      return _invokableGenerator.StartedTask();
    }

    public Task<T> StartedTask<T>()
    {
      return _invokableGenerator.StartedTask<T>();
    }

    public Func<T> Func<T>()
    {
      return _invokableGenerator.Func<T>();
    }

    public Func<T1, T2> Func<T1, T2>()
    {
      return _invokableGenerator.Func<T1, T2>();
    }

    public Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return _invokableGenerator.Func<T1, T2, T3>();
    }

    public Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return _invokableGenerator.Func<T1, T2, T3, T4>();
    }

    public Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return _invokableGenerator.Func<T1, T2, T3, T4, T5>();
    }

    public Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return _invokableGenerator.Func<T1, T2, T3, T4, T5, T6>();
    }

    public Action Action()
    {
      return _invokableGenerator.Action();
    }

    public Action<T> Action<T>()
    {
      return _invokableGenerator.Action<T>();
    }

    public Action<T1, T2> Action<T1, T2>()
    {
      return _invokableGenerator.Action<T1, T2>();
    }

    public Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return _invokableGenerator.Action<T1, T2, T3>();
    }

    public Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return _invokableGenerator.Action<T1, T2, T3, T4>();
    }

    public Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return _invokableGenerator.Action<T1, T2, T3, T4, T5>();
    }

    public Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return _invokableGenerator.Action<T1, T2, T3, T4, T5, T6>();
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
        typeof (KeyValuePair<,>).MakeGenericType(keyType, valueType), _proxyBasedGenerator.Instance(keyType), _proxyBasedGenerator.Instance(valueType)
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
      return _numericGenerator.Digit();
    }

    public int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      return _numericGenerator.IntegerFromSequence(startingValue, step);
    }

    public byte Octet()
    {
      return _numericGenerator.Octet();
    }

    public int IntegerDivisibleBy(int quotient)
    {
      return _numericGenerator.IntegerDivisibleBy(quotient);
    }

    public int IntegerNotDivisibleBy(int quotient)
    {
      return _numericGenerator.IntegerNotDivisibleBy(quotient);
    }

    public int IntegerWithExactDigitsCount(int digitsCount)
    {
      return _numericGenerator.IntegerWithExactDigitsCount(digitsCount);
    }

    public long LongIntegerWithExactDigitsCount(int digitsCount)
    {
      return _numericGenerator.LongIntegerWithExactDigitsCount(digitsCount);
    }

    public uint UnsignedIntegerWithExactDigitsCount(int digitsCount)
    {
      return _numericGenerator.UnsignedIntegerWithExactDigitsCount(digitsCount);
    }

    public ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount)
    {
      return _numericGenerator.UnsignedLongIntegerWithExactDigitsCount(digitsCount);
    }

    public byte PositiveDigit()
    {
      return _numericGenerator.PositiveDigit();
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
      return _proxyBasedGenerator.Exploding<T>();
    }

    public int Integer()
    {
      return _numericGenerator.Integer();
    }

    public double Double()
    {
      return _numericGenerator.Double();
    }

    public double DoubleOtherThan(double[] others)
    {
      return _numericGenerator.DoubleOtherThan(others);
    }

    public long LongInteger()
    {
      return _numericGenerator.LongInteger();
    }

    public long LongIntegerOtherThan(long[] others)
    {
      return _numericGenerator.LongIntegerOtherThan(others);
    }

    public ulong UnsignedLongInteger()
    {
      return _numericGenerator.UnsignedLongInteger();
    }

    public ulong UnsignedLongIntegerOtherThan(ulong[] others)
    {
      return _numericGenerator.UnsignedLongIntegerOtherThan(others);
    }

    public int IntegerOtherThan(int[] others)
    {
      return _numericGenerator.IntegerOtherThan(others);
    }

    public byte Byte()
    {
      return _numericGenerator.Byte();
    }

    public byte ByteOtherThan(byte[] others)
    {
      return _numericGenerator.ByteOtherThan(others);
    }

    public decimal Decimal()
    {
      return _numericGenerator.Decimal();
    }

    public decimal DecimalOtherThan(decimal[] others)
    {
      return _numericGenerator.DecimalOtherThan(others);
    }

    public uint UnsignedInt()
    {
      return _numericGenerator.UnsignedInt();
    }

    public uint UnsignedIntOtherThan(uint[] others)
    {
      return _numericGenerator.UnsignedIntOtherThan(others);
    }

    public ushort UnsignedShort()
    {
      return _numericGenerator.UnsignedShort();
    }

    public ushort UnsignedShortOtherThan(ushort[] others)
    {
      return _numericGenerator.UnsignedShortOtherThan(others);
    }

    public short ShortInteger()
    {
      return _numericGenerator.ShortInteger();
    }

    public short ShortIntegerOtherThan(short[] others)
    {
      return _numericGenerator.ShortIntegerOtherThan(others);
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
      return _proxyBasedGenerator.ResultOfGenericVersionOfMethod<Any>(type, name);
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