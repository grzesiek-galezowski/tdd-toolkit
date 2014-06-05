using System;

namespace TddEbook.TddToolkitSpecification
{
  public class IncrementalType : IEquatable<IncrementalType>
  {
    public IncrementalType(int x, string y)
    {
      X = x;
      Y = y;
    }

    public string Y { get; set; }

    public int X { get; set; }

    public static bool operator ==(IncrementalType a, IncrementalType b)
    {
      return false;
    }

    public static bool operator !=(IncrementalType a, IncrementalType b)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      return false;
    }

    public override int GetHashCode()
    {
      return 0;
    }

    public bool Equals(IncrementalType other)
    {
      throw new NotImplementedException();
    }
  }
}
