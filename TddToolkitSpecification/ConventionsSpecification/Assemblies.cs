using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestStack.ConventionTests;

namespace TddEbook.TddToolkitSpecification.ConventionsSpecification
{
  public class Assemblies : IConventionData
  {
    public Assemblies(params Assembly[] checkedAssemblies)
    {
      CheckedAssemblies = checkedAssemblies;
    }

    private Assembly[] CheckedAssemblies { get; }
    public bool HasData { get; } = true;

    public string Description
      => string.Join(Environment.NewLine, CheckedAssemblies.Select(v => v.ToString()));

    public override string ToString()
    {
      return Description;
    }

    private bool AssemblyHasForbiddenReferences(
      Assembly sourceAssembly, Assembly forbiddenReference)
    {
      return sourceAssembly.GetReferencedAssemblies().Any(
        referencedAssemblies => IsForbidden(referencedAssemblies, forbiddenReference));
    }

    private bool IsForbidden(AssemblyName reference, Assembly assembly)
    {
      return reference.ToString() == assembly.GetName().ToString();
    }

    public IEnumerable<Assemblies> Referencing(Assembly forbiddenReference)
    {
      return CheckedAssemblies.Where(sourceAssembly => AssemblyHasForbiddenReferences(
          sourceAssembly, forbiddenReference))
        .Select(a => new Assemblies(a));
    }
  }
}