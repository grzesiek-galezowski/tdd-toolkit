using System.Linq;
using TddEbook.TypeReflection;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace Conventions
{
  public class AllClassesHaveAtMostOneConstructor : IConvention<Types>
  {
    public void Execute(Types data, IConventionResultContext result)
    {
      result.Is("Each type must have at most one constructor",
        data.TypesToVerify.Select(SmartType.For)
        .Where(t => !t.IsException())
        .Where(t => !t.HasPublicConstructorCountOfAtMost(1))
        .Select(t => t.ToClrType()));
    }

    public string ConventionReason { get; } = "Does not apply to exceptions. " +
                                              "For more ways to create objects, use static factory methods " +
                                              "or other creational patterns";
  }
}