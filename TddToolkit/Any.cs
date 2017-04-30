using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Net;
using System.Threading.Tasks;
using TddEbook.TddToolkit.Subgenerators;
using Type = System.Type;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    private static readonly AllGenerator _any = AllGenerator.Create();

    public static IPAddress IpAddress()
    {
      return _any.IpAddress();
    }

    public static T ValueOtherThan<T>(params T[] omittedValues)
    {
      return _any.ValueOtherThan(omittedValues);
    }

    public static T From<T>(params T[] possibleValues)
    {
      return _any.From(possibleValues);
    }

    public static DateTime DateTime()
    {
      return _any.DateTime();
    }

    public static TimeSpan TimeSpan()
    {
      return _any.TimeSpan();
    }

    public static T ValueOf<T>()
    {
      return _any.ValueOf<T>();
    }

    public static IEnumerable<T> EmptyEnumerableOf<T>()
    {
      return _any.EmptyEnumerableOf<T>();
    }

    public static string LegalXmlTagName()
    {
      return _any.LegalXmlTagName();
    }

    public static bool Boolean()
    {
      return _any.Boolean();
    }

    public static object Object()
    {
      return _any.Object();
    }

    public static T Exploding<T>() where T : class
    {
      return _any.Exploding<T>();
    }

    public static MethodInfo Method()
    {
      return _any.Method();
    }

    public static Type Type()
    {
      return _any.Type();
    }

    public static T InstanceOf<T>()
    {
      return _any.InstanceOf<T>();
    }

    public static T Instance<T>()
    {
      return _any.Instance<T>();
    }

    public static T Dummy<T>()
    {
      return _any.Dummy<T>();
    }

#pragma warning disable CC0068 // Unused Method
#pragma warning disable S1144 // Unused private types or members should be removed
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private static T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return _any.InstanceOtherThanObjects<T>(omittedValues);
    }
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore CC0068 // Unused Method

    public static T SubstituteOf<T>() where T : class
    {
      return _any.SubstituteOf<T>();
    }

    public static T OtherThan<T>(params T[] omittedValues)
    {
      return _any.OtherThan(omittedValues);
    }

    public static Uri Uri()
    {
      return _any.Uri();
    }

    public static Guid Guid()
    {
      return _any.Guid();
    }

    public static string UrlString()
    {
      return _any.UrlString();
    }

    public static Exception Exception()
    {
      return _any.Exception();
    }

    public static int Port()
    {
      return _any.Port();
    }

    public static string Ip()
    {
      return _any.Ip();
    }

    public static object Instance(Type type)
    {
      return _any.Instance(type);
    }

    public static object InstanceOtherThanObjects(Type type, params object[] omittedValues)
    {
      return _any.InstanceOtherThanObjects(type, omittedValues);
    }

    public static IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      return _any.EnumerableWith(included);
    }

    public static Task NotStartedTask()
    {
      return _any.NotStartedTask();
    }

    public static Task<T> NotStartedTask<T>()
    {
      return _any.NotStartedTask<T>();
    }

    public static Task StartedTask()
    {
      return _any.StartedTask();
    }

    public static Task<T> StartedTask<T>()
    {
      return _any.StartedTask<T>();
    }

    public static Func<T> Func<T>()
    {
      return _any.Func<T>();
    }
    public static Func<T1, T2> Func<T1, T2>()
    {
      return _any.Func<T1, T2>();
    }

    public static Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return _any.Func<T1, T2, T3>();
    }

    public static Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return _any.Func<T1, T2, T3, T4>();
    }

    public static Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return _any.Func<T1, T2, T3, T4, T5>();
    }

    public static Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return _any.Func<T1, T2, T3, T4, T5, T6>();
    }

    public static Action Action()
    {
      return _any.Action();
    }

    public static Action<T> Action<T>()
    {
      return _any.Action<T>();
    }
    public static Action<T1, T2> Action<T1, T2>()
    {
      return _any.Action<T1, T2>();
    }

    public static Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return _any.Action<T1, T2, T3>();
    }

    public static Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return _any.Action<T1, T2, T3, T4>();
    }

    public static Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return _any.Action<T1, T2, T3, T4, T5>();
    }

    public static Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return _any.Action<T1, T2, T3, T4, T5, T6>();
    }
  }


}

