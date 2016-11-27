using System.Collections.Generic;

namespace TddEbook.TddToolkitSpecification.Fixtures
{
  public interface RecursiveInterface
  {
    List<RecursiveInterface> GetNestedWithArguments(int a, int b);
    List<RecursiveInterface> GetNested();
    void VoidMethod();
    IDictionary<string, RecursiveInterface> NestedAsDictionary { get; }
    RecursiveInterface Nested { get; }
    int Number { get; }
    int GetNumber();
  }
}