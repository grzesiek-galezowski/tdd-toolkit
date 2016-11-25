using System.Reflection;

namespace ConventionsSpecification
{
  internal static class ExpectedAssemblyRefConventionMessage
  {
    public static string Start(Assembly forbiddenReference, Assembly sourceAssembly, string reason)
    {
      return $@"'Forbidden reference to {forbiddenReference} because {reason}' for 'Assemblies in {sourceAssembly}'".Replace("\t", " ");
    }

    public static string End(Assembly sourceAssembly)
    {
      return $@"{sourceAssembly}{"\r\n"}";
    }
  }
}