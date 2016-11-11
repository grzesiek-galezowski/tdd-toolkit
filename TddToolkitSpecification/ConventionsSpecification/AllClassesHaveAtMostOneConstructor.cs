using System.Linq;
using System.Reflection;
using NUnit.Framework.Internal;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace TddEbook.TddToolkitSpecification.ConventionsSpecification
{
  public class AllClassesHaveAtMostOneConstructor : IConvention<Types>
  {
    public void Execute(Types data, IConventionResultContext result)
    {
      result.Is("Each type must have at most one constructor",
        data.TypesToVerify.Select(TypeReflection.SmartType.For)
        .Where(t => !t.IsException())
        .Where(t => t.HasPublicConstructorCountOfAtMost(1)));
    }

    public string ConventionReason { get; } = "Does not apply to exceptions. " +
                                              "For more ways to create objects, use static factory methods " +
                                              "or other creational patterns";
  }
}