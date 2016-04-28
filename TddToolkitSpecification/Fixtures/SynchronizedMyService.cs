namespace TddEbook.TddToolkitSpecification
{
  public abstract class SynchronizedMyService<T> : MyService
  {
    private readonly MyService _innerInstance;

    protected SynchronizedMyService(MyService innerInstance, T aLock)
    {
      _innerInstance = innerInstance;
      Lock = aLock;
    }

    public T Lock
    {
      get; private set;
    }

    public void VoidCall(int i)
    {
      try
      {
        EnterLock();
        _innerInstance.VoidCall(i);
      }
      finally
      {
        ExitLock();
      }
    }

    public void VoidCallNotExitedOnException(int i)
    {
      EnterLock();
      _innerInstance.VoidCall(i);
      ExitLock();
    }

    public void VoidCallNotEntered(int i)
    {
      _innerInstance.VoidCall(i);
    }

    public void VoidCallNotExited(int i)
    {
      EnterLock();
      _innerInstance.VoidCall(i);
    }

    protected abstract void ExitLock();
    protected abstract void EnterLock();

    public int CallWithResult(string alabama)
    {
      try
      {
        EnterLock();
        return _innerInstance.CallWithResult(alabama);
      }
      finally
      {
        ExitLock();
      }
    }

    public int CallWithResultNotEntered(string alabama)
    {
      return _innerInstance.CallWithResult(alabama);
    }

    public int CallWithResultNotExited(string alabama)
    {
      EnterLock();
      return _innerInstance.CallWithResult(alabama);
    }

    public int CallWithResultNotExitedOnException(string alabama)
    {
      EnterLock();
      var result = _innerInstance.CallWithResult(alabama);
      ExitLock();
      return result;
    }
  }
}