using System;

namespace TddEbook.TddToolkitSpecification.Fixtures
{
  [Serializable]
  public class ObjectWithCopyConstructor
  {
    internal string _field;

    public ObjectWithCopyConstructor(ObjectWithCopyConstructor o)
    {
      this._field = o._field;
    }
  }
}