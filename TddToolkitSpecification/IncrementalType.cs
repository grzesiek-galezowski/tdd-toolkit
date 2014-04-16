using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddToolkitSpecification
{
  public class IncrementalType : IEquatable<IncrementalType>
  {
    public IncrementalType(int x, string y)
    {
      _x = x;
      _y = y;
    }

    public string _y { get; set; }

    public int _x { get; set; }

    public static bool operator ==(IncrementalType a, IncrementalType b)
    {
      throw new Exception();
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
