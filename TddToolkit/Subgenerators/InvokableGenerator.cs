using System;
using System.Threading.Tasks;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class InvokableGenerator
  {
    private readonly IProxyBasedGenerator _proxyBasedGenerator;

    public InvokableGenerator(IProxyBasedGenerator proxyBasedGenerator)
    {
      _proxyBasedGenerator = proxyBasedGenerator;
    }

    public Task NotStartedTask()
    {
      return new Task(() => Task.Delay(1).Wait());
    }

    public Task<T> NotStartedTask<T>()
    {
      return new Task<T>(_proxyBasedGenerator.Instance<T>);
    }

    public Task StartedTask()
    {
      return Clone.Of(Task.Delay(0));
    }

    public Task<T> StartedTask<T>()
    {
      return Task.FromResult(_proxyBasedGenerator.Instance<T>());
    }

    public Func<T> Func<T>()
    {
      return _proxyBasedGenerator.Instance<Func<T>>();
    }

    public Func<T1, T2> Func<T1, T2>()
    {
      return _proxyBasedGenerator.Instance<Func<T1, T2>>();
    }

    public Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return _proxyBasedGenerator.Instance<Func<T1, T2, T3>>();
    }

    public Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return _proxyBasedGenerator.Instance<Func<T1, T2, T3, T4>>();
    }

    public Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return _proxyBasedGenerator.Instance<Func<T1, T2, T3, T4, T5>>();
    }

    public Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return _proxyBasedGenerator.Instance<Func<T1, T2, T3, T4, T5, T6>>();
    }

    public Action Action()
    {
      return _proxyBasedGenerator.Instance<Action>();
    }

    public Action<T> Action<T>()
    {
      return _proxyBasedGenerator.Instance<Action<T>>();
    }

    public Action<T1, T2> Action<T1, T2>()
    {
      return _proxyBasedGenerator.Instance<Action<T1, T2>>();
    }

    public Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return _proxyBasedGenerator.Instance<Action<T1, T2, T3>>();
    }

    public Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return _proxyBasedGenerator.Instance<Action<T1, T2, T3, T4>>();
    }

    public Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return _proxyBasedGenerator.Instance<Action<T1, T2, T3, T4, T5>>();
    }

    public Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return _proxyBasedGenerator.Instance<Action<T1, T2, T3, T4, T5, T6>>();
    }
  }
}