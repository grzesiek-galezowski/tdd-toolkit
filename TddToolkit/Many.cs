using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddEbook.TddToolkit
{
  public class Many
  {
    public void Times(Action action)
    {
      3.Times(action);
    }
  }

  public static class TimesExtensions
  {
    public static void Times(this int times, Action action)
    {
      for (int i = 0; i < times; ++i)
      {
        action.Invoke();
      }
    }
  }
}
