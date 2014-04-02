using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddEbook.TddToolkit
{
  public class WrapperDuo<T>
  {
    public T Prototype { get; private set; }
    public T Object { get; private set; } 

    public WrapperDuo(T original, T wrapped)
    {
      this.Prototype = original;
      this.Object = wrapped;
    }

    public static WrapperDuo<T> With(T original, T wrapped)
    {
      return new WrapperDuo<T>(original, wrapped);
    }
  }
}
