using System;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {

    public static T Of<T>() where T : struct, IConvertible
    {
      return _any.Of<T>();
    }

    /// <typeparam name="T">MUST BE AN ENUM. FOR NORMAL VALUES, USE Any.OtherThan()</typeparam>
    /// <param name="excludedValues"></param>
    /// <returns></returns>
    public static T Besides<[MustBeAnEnum] T>([MustBeAnEnum] params T[] excludedValues) where T : struct, IConvertible
    {
      return _any.Besides(excludedValues);
    }
  }

  public class MustBeAnEnumAttribute : Attribute
  {
  }
}
