using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TddEbook.TddToolkit.ImplementationDetails.Spying
{
  public class SpiedCalls<T>
  {
    private ISpyable<T> spyable;

    public SpiedCalls(ISpyable<T> spyable)
    {
      this.spyable = spyable;
    }

    public WhenSpyingCall<T> When(Expression<Action<T>> methodCall)
    {
      return new WhenSpyingCall<T>(spyable, methodCall);
    }

    public static SpiedCalls<T> To(ISpyable<T> spyable)
    {
      return new SpiedCalls<T>(spyable);
    }
  }

}
