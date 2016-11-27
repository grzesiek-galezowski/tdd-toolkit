using System;

namespace TddEbook.TddToolkitSpecification.Fixtures
{
  [Serializable]
  public abstract class AbstractObjectWithVirtualMethods
  {
    public virtual string GetSomething()
    {
      return default(string);
    }

    public virtual string GetSomething2()
    {
      return "something";
    }

    public virtual string GetSomethingButThrowExceptionWhileGettingIt()
    {
      throw new Exception("Let'_field suppose dummy data cause this method to throw exception");
    }
  }
}