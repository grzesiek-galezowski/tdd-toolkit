using System;

namespace TddEbook.TddToolkit.CommonTypes
{
  public static class Maybe
  {
    public static Maybe<T> Wrap<T>(T instance) where T : class
    {
      return new Maybe<T>(instance);
    }
  }

  public struct Maybe<T> where T : class
  {
    private readonly T _value;
    private static readonly Maybe<T> _not = new Maybe<T>();

    public Maybe(T instance)
      : this()
    {
      if (instance != null)
      {
        HasValue = true;
        _value = instance;
      }
    }

    public bool HasValue { get; private set; }

    public T Value()
    {
      if (HasValue)
      {
        return _value;
      }
      else
      {
        throw new Exception("No instance of type " + typeof(T));
      }
    }

    public static Maybe<T> Not
    {
      get { return _not; }
    }

    public T ValueOr(T alternativeValue)
    {
      return HasValue ? Value() : alternativeValue;
    }

    public Maybe<T> Otherwise(Maybe<T> alternative)
    {
      return HasValue ? this : alternative;
    }

    public static implicit operator Maybe<T>(T instance)
    {
      return Maybe.Wrap(instance);
    }

    public override string ToString()
    {
      return HasValue ? Value().ToString() : "<Nothing>";
    }

    public Maybe<U> To<U>() where U : class
    {
      if (!HasValue)
      {
        return Maybe<U>.Not;
      }
      else
      {
        return Maybe.Wrap(Value() as U);
      }
    }
  }

}
