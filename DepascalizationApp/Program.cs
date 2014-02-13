using System.IO;
using CommandLine;

namespace DepascalizationApp
{
  public static class Program
  {
    static void Main(string[] args)
    {
      var options = new Options();
      if (Parser.Default.ParseArguments(args, options))
      {
        var fileContent = File.ReadAllText(options.InputFile);
        var outputXml = new Depascalization.Depascalization().OfNUnitReport(fileContent);
        File.WriteAllText(options.OutputFile, outputXml);
      }
    }
  }
}
