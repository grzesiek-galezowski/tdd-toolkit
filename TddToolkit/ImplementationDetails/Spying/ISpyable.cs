using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TddEbook.TddToolkit.ImplementationDetails.Spying
{
  public interface ISpyable<T>
  {
    void When(Expression<Action<T>> methodCall, Action<object[]> actionToPerform);
  }
}
