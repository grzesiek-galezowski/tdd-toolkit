using System;
using System.Threading.Tasks;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class InvokableGenerator
  {
    public Task NotStartedTask()
    {
      return new Task(() => Task.Delay(1).Wait());
    }

    public Task<T> NotStartedTask<T>(IInstanceGenerator instanceGenerator)
    {
      return new Task<T>(instanceGenerator.Instance<T>);
    }

    public Task StartedTask()
    {
      return Clone.Of(Task.Delay(0));
    }

    public Task<T> StartedTask<T>(ProxyBasedGenerator genericGenerator)
    {
      return Task.FromResult(genericGenerator.Instance<T>());
    }

    public Func<T> Func<T>(IInstanceGenerator instanceGenerator)
    {

      return instanceGenerator.Instance<Func<T>>();
    }

    public Func<T1, T2> Func<T1, T2>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Func<T1, T2>>();
    }

    public Func<T1, T2, T3> Func<T1, T2, T3>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Func<T1, T2, T3>>();
    }

    public Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Func<T1, T2, T3, T4>>();
    }

    public Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Func<T1, T2, T3, T4, T5>>();
    }

    public Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Func<T1, T2, T3, T4, T5, T6>>();
    }

    public Action Action(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action>();
    }

    public Action<T> Action<T>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T>>();
    }

    public Action<T1, T2> Action<T1, T2>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T1, T2>>();
    }

    public Action<T1, T2, T3> Action<T1, T2, T3>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T1, T2, T3>>();
    }

    public Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T1, T2, T3, T4>>();
    }

    public Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T1, T2, T3, T4, T5>>();
    }

    public Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>(IInstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T1, T2, T3, T4, T5, T6>>();
    }
  }
}