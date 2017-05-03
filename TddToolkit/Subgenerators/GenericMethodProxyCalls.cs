using System;
using System.Linq;
using System.Reflection;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class GenericMethodProxyCalls
  {
    public object ResultOfGenericVersionOfMethod<T>(T instance, Type genericArgumentType, string name)
    {
      return typeof(T).GetMethods().Where(NameIs(name))
        .First(ParameterlessGenericVersion()).MakeGenericMethod(genericArgumentType).Invoke(instance, null);
    }

    public static Func<MethodInfo, bool> ParameterlessGenericVersion()
    {
      return m => !m.GetParameters().Any() && m.IsGenericMethodDefinition;
    }

    public static Func<MethodInfo, bool> NameIs(string name)
    {
      return m => m.Name == name;
    }

    public object ResultOfGenericVersionOfMethod<T>(
      T instance, Type type1, Type type2, string name)
    {
      return ResultOfGenericVersionOfMethod(instance, type1, type2, name, new object[]{});
    }

    public object ResultOfGenericVersionOfMethod<T>(
      T instance, Type type1, Type type2, string name, object[] parameters)
    {
      var method = FindEmptyGenericsInstanceMethod<T>(name, parameters.Length);

      var genericMethod = method.MakeGenericMethod(type1, type2);

      return genericMethod.Invoke(instance, parameters);
    }


    public MethodInfo FindEmptyGenericsInstanceMethod<T>(
      string name, int parametersLength)
    {
      var methods = typeof(T).GetMethods(
          BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(m => m.IsGenericMethodDefinition)
        .Where(m => m.GetParameters().Length == parametersLength);
      var method = methods.First(m => m.Name == name);
      return method;
    }
  }
}