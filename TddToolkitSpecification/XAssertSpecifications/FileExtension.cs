using System;

namespace TddEbook.TddToolkitSpecification.XAssertSpecifications
{
  public class FileExtension
    : IEquatable<FileExtension>
  {
    private readonly string _extension;

    internal FileExtension(string extension)
    {
      this._extension = extension;
    }

    public override string ToString()
    {
      return _extension;
    }

    public static FileExtension Value(string extensionString)
    {
      FileExtensionAssert.NotNull(extensionString);
      FileExtensionAssert.NotEmpty(extensionString);
      FileExtensionAssert.Valid(extensionString);

      return new FileExtension(extensionString);
    }

    public bool Equals(FileExtension other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_extension, other._extension);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((FileExtension)obj);
    }

    public override int GetHashCode()
    {
      return (_extension != null ? _extension.GetHashCode() : 0);
    }

    public static bool operator ==(FileExtension left, FileExtension right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(FileExtension left, FileExtension right)
    {
      return !Equals(left, right);
    }

  }
}