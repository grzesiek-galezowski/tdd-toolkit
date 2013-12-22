using System.Collections.Generic;

namespace TddEbook.TddToolkit
{
  public class Empty
  {
    public static List<T> List<T>()
    {
      return new List<T>();
    }

    public static ICollection<T> Collection<T>()
    {
      return List<T>();
    }

    public static T[] Array<T>()
    {
      return new T[0];
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return new Dictionary<TKey, TValue>();
    }

    public static IEnumerable<T> Enumerable<T>()
    {
      return List<T>();
    }

    public static string String
    {
      get
      {
        return string.Empty;
      }
    }
   
  }
}
