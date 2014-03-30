using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TddEbook.TddToolkit.ImplementationDetails.Spying;

namespace TddEbook.TddToolkit
{
  public class Spy
  {
    public static SpiedCalls<T> On<T>(T spyableImplementation)
    {
      ISpyable<T> spyable = ((ISpyable<T>)spyableImplementation);
      return SpiedCalls<T>.To(spyable);
    }
  }

  
}
