using System;

namespace TddEbook.TddToolkitSpecification.Fixtures
{
  public class ConcreteDataStructure
  {
    public TimeSpan Span { get; set; }
    public ConcreteDataStructure2 Data { get; set; }

    public ConcreteDataStructure2 _field;
  }
}