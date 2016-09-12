using System;
using System.IO;

namespace TddEbook.TddToolkitSpecification.XAssertSpecifications
{
  public class DirectoryPath : IEquatable<DirectoryPath>
  {
    private readonly string _path;

    internal DirectoryPath(string path)
    {
      _path = path;
    }

    public DirectoryPath(DirectoryPath path, string directoryName)
      : this(Path.Combine(path.ToString(), directoryName.ToString()))
    {

    }

    public override string ToString()
    {
      return _path;
    }

    public bool Equals(DirectoryPath other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_path, other._path);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((DirectoryPath) obj);
    }

    public override int GetHashCode()
    {
      return _path.GetHashCode();
    }

    public static bool operator ==(DirectoryPath left, DirectoryPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(DirectoryPath left, DirectoryPath right)
    {
      return !Equals(left, right);
    }

  }
}