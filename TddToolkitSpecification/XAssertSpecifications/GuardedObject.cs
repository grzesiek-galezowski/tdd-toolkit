using System;

namespace TddEbook.TddToolkitSpecification.XAssertSpecifications
{
  public class GuardedObject
  {
    public GuardedObject(int a, string b, int c, string dede)
    {
      ArgumentNotNull(b, "x");
      ArgumentNotNull(dede, "y");
    }

    public static void ArgumentNotNull(object value, string name)
    {
      if (value == null)
        throw new ArgumentNullException("Argument " + name + " must not be null", name);
    }
  }
}