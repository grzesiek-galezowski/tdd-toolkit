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
    private static readonly AllGenerator Generate = AllGeneratorFactory.Create();

    public static IPAddress IpAddress()
    {
      return Generate.IpAddress();
    }

    public static T ValueOtherThan<T>(params T[] omittedValues)
    {
      return Generate.ValueOtherThan(omittedValues);
    }

    public static T From<T>(params T[] possibleValues)
    {
      return Generate.From(possibleValues);
    }

    public static DateTime DateTime()
    {
      return Generate.DateTime();
    }

    public static TimeSpan TimeSpan()
    {
      return Generate.TimeSpan();
    }

    public static T ValueOf<T>()
    {
      return Generate.ValueOf<T>();
    }

    public static IEnumerable<T> EmptyEnumerableOf<T>()
    {
      return Generate.EmptyEnumerableOf<T>();
    }

    public static string LegalXmlTagName()
    {
      return Generate.LegalXmlTagName();
    }

    public static bool Boolean()
    {
      return Generate.Boolean();
    }

    public static object Object()
    {
      return Generate.Object();
    }

    public static T Exploding<T>() where T : class
    {
      return Generate.Exploding<T>();
    }

    public static MethodInfo Method()
    {
      return Generate.Method();
    }

    public static Type Type()
    {
      return Generate.Type();
    }

    public static T InstanceOf<T>()
    {
      return Generate.InstanceOf<T>();
    }

    public static T Instance<T>()
    {
      return Generate.Instance<T>();
    }

    public static T Dummy<T>()
    {
      return Generate.Dummy<T>();
    }

#pragma warning disable CC0068 // Unused Method
#pragma warning disable S1144 // Unused private types or members should be removed
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private static T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return Generate.InstanceOtherThanObjects<T>(omittedValues);
    }
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore CC0068 // Unused Method

    public static T SubstituteOf<T>() where T : class
    {
      return Generate.SubstituteOf<T>();
    }

    public static T OtherThan<T>(params T[] omittedValues)
    {
      return Generate.OtherThan(omittedValues);
    }

    public static Uri Uri()
    {
      return Generate.Uri();
    }

    public static Guid Guid()
    {
      return Generate.Guid();
    }

    public static string UrlString()
    {
      return Generate.UrlString();
    }

    public static Exception Exception()
    {
      return Generate.Exception();
    }

    public static int Port()
    {
      return Generate.Port();
    }

    public static string Ip()
    {
      return Generate.Ip();
    }

    public static object Instance(Type type)
    {
      return Generate.Instance(type);
    }

    public static object InstanceOtherThanObjects(Type type, params object[] omittedValues)
    {
      return Generate.InstanceOtherThanObjects(type, omittedValues);
    }

    public static IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      return Generate.EnumerableWith(included);
    }

    public static Task NotStartedTask()
    {
      return Generate.NotStartedTask();
    }

    public static Task<T> NotStartedTask<T>()
    {
      return Generate.NotStartedTask<T>();
    }

    public static Task StartedTask()
    {
      return Generate.StartedTask();
    }

    public static Task<T> StartedTask<T>()
    {
      return Generate.StartedTask<T>();
    }

    public static Func<T> Func<T>()
    {
      return Generate.Func<T>();
    }
    public static Func<T1, T2> Func<T1, T2>()
    {
      return Generate.Func<T1, T2>();
    }

    public static Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return Generate.Func<T1, T2, T3>();
    }

    public static Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return Generate.Func<T1, T2, T3, T4>();
    }

    public static Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return Generate.Func<T1, T2, T3, T4, T5>();
    }

    public static Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return Generate.Func<T1, T2, T3, T4, T5, T6>();
    }

    public static Action Action()
    {
      return Generate.Action();
    }

    public static Action<T> Action<T>()
    {
      return Generate.Action<T>();
    }
    public static Action<T1, T2> Action<T1, T2>()
    {
      return Generate.Action<T1, T2>();
    }

    public static Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return Generate.Action<T1, T2, T3>();
    }

    public static Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return Generate.Action<T1, T2, T3, T4>();
    }

    public static Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return Generate.Action<T1, T2, T3, T4, T5>();
    }

    public static Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return Generate.Action<T1, T2, T3, T4, T5, T6>();
    }
  }


}

