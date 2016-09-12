using System;
using System.IO;

namespace TddEbook.TddToolkitSpecification.XAssertSpecifications
{
  static internal class FileExtensionAssert
  {
    internal static void Valid(string extensionString)
    {
      if (Path.GetExtension(extensionString) != extensionString)
      {
        throw new ArgumentException("Invalid extension " + extensionString, "extensionString");
      }
    }

    internal static void NotEmpty(string extensionString)
    {
      if (extensionString == String.Empty)
      {
        throw new ArgumentException("Tried to create an extension with empty value");
      }
    }

    internal static void NotNull(string extensionString)
    {
      if (extensionString == null)
      {
        throw new ArgumentException("Tried to create an extension with null value");
      }
    }
  }
}