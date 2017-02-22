using System;
using System.Linq;
using System.Reflection;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class GenericMethodProxyCalls
  {
    public GenericMethodProxyCalls()
    {
    }

    public object ResultOfGenericVersionOfMethod<T>(Type type, string name)
    {
      return typeof(T).GetMethods().Where(NameIs(name))
        .First(ParameterlessGenericVersion()).MakeGenericMethod(type).Invoke(null, null);
    }

    public static Func<MethodInfo, bool> ParameterlessGenericVersion()
    {
      return m => !m.GetParameters().Any() && m.IsGenericMethodDefinition;
    }

    public static Func<MethodInfo, bool> NameIs(string name)
    {
      return m => m.Name == name;
    }

    public object ResultOfGenericVersionOfMethod<T>(Type type, string name, object[] args)
    {
      var method = FindEmptyGenericsMethod<T>(name);

      var genericMethod = method.MakeGenericMethod(type);

      return genericMethod.Invoke(null, args);
    }

    public object ResultOfGenericVersionOfMethod<T>(Type type1, Type type2, string name)
    {
      var method = FindEmptyGenericsMethod<T>(name);

      var genericMethod = method.MakeGenericMethod(type1, type2);

      return genericMethod.Invoke(null, null);
    }

    public object ResultOfGenericVersionOfMethod(Type type, string name)
    {
      return ResultOfGenericVersionOfMethod<Any>(type, name);
    }

    public MethodInfo FindEmptyGenericsMethod<T>(string name)
    {
      var methods = typeof(T).GetMethods(
          BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        .Where(m => m.IsGenericMethodDefinition)
        .Where(m => !m.GetParameters().Any());
      var method = methods.First(m => m.Name == name);
      return method;
    }

  }
}