using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TddEbook.TddToolkit.ImplementationDetails.Spying
{
  public class WhenSpyingCall<T>
  {
    private Expression<Action<T>> _methodCall;
    private ISpyable<T> _spyable;

    public WhenSpyingCall(ISpyable<T> spyable, Expression<Action<T>> methodCall)
    {
      this._spyable = spyable;
      this._methodCall = methodCall;
    }

    public void Do(Action<object[]> actionToPerform)
    {
      _spyable.When(_methodCall, actionToPerform); 
    }
  }
}
