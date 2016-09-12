namespace TddEbook.TddToolkitSpecification.XAssertSpecifications
{
  public class FileName
  {
    private readonly string _path;

    internal FileName(string path)
    {
      _path = path;
    }

    public FileName(FileNameWithoutExtension nameWithoutExtension, FileExtension extension)
      : this(nameWithoutExtension.ToString() + extension.ToString())
    {

    }

  }
}