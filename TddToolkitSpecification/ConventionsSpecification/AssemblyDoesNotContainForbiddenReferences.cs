using System.Collections.Generic;
using System.Reflection;
using TestStack.ConventionTests;

namespace TddEbook.TddToolkitSpecification.ConventionsSpecification
{
  public class AssemblyDoesNotContainForbiddenReferences : IConvention<Assemblies>
  {
    private readonly Assembly _forbiddenReference;
    private readonly string _reason;

    public AssemblyDoesNotContainForbiddenReferences(Assembly forbiddenReference, string reason)
    {
      _forbiddenReference = forbiddenReference;
      _reason = reason;
    }

    public string ConventionReason => "Some architectures (like ports & adapters) don't allow some " +
                                      "references to occur (e.g. domain cannot reference adapters). " +
                                      "Another use for this reference is saying that no project other " +
                                      "than composition root should have a reference to IoC container.";

    public void Execute(Assemblies assemblies, IConventionResultContext result)
    {
      result.Is($"Forbidden reference to {_forbiddenReference}"
        + Because()
        , assemblies.Referencing(_forbiddenReference));
    }

    private string Because()
    {
      if (_reason == "")
      {
        return "";
      }
      return $" because {_reason}";
    }
  }
}