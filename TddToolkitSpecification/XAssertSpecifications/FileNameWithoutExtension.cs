using System;

namespace TddEbook.TddToolkitSpecification.XAssertSpecifications
{
  public class FileNameWithoutExtension : IEquatable<FileNameWithoutExtension>
  {
    public static FileNameWithoutExtension Value(string fileNameWithoutExtensionString)
    {
      return new FileNameWithoutExtension(fileNameWithoutExtensionString);
    }

    private readonly string _value;

    internal FileNameWithoutExtension(string value)
    {
      _value = value;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((FileNameWithoutExtension)obj);
    }

    public bool Equals(FileNameWithoutExtension other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_value, other._value);
    }

    public override int GetHashCode()
    {
      return _value.GetHashCode();
    }

    public static bool operator ==(FileNameWithoutExtension left, FileNameWithoutExtension right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(FileNameWithoutExtension left, FileNameWithoutExtension right)
    {
      return !Equals(left, right);
    }

    public override string ToString()
    {
      return _value;
    }
  }
}