using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddEbook.TddToolkit
{
    public class AnyData
    {
      protected T Any<T>()
      {
        return TddEbook.TddToolkit.Any.Instance<T>();
      }
    }

}
