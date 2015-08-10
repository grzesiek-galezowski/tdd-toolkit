using System;
using System.IO;
using CommandLine;

namespace DepascalizationApp
{
  public static class Program
  {
    static int Main(string[] args)
    {
      var result = Parser.Default.ParseArguments<Options>(args);

      var exitCode = result.Return(options => 
      {
        var fileContent = File.ReadAllText(options.InputFile);
        var outputXml = new Depascalization.Depascalization().OfNUnitReport(fileContent);
        File.WriteAllText(options.OutputFile, outputXml);
        return 0;
      },
      errors => 1);

      return exitCode;
    }
  }
}
