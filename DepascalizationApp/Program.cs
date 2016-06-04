using System;
using System.IO;
using System.Linq;
using CommandLine;
using Depascalization;

namespace DepascalizationApp
{
  public static class Program
  {
    static int Main(string[] args)
    {
      var result = Parser.Default.ParseArguments<Options>(args);
      var exitCode = 0;

      result.WithParsed(options => 
      {
        var fileContent = File.ReadAllText(options.InputFile);
        var outputXml = new Transformation().OfNUnitReport(fileContent);
        File.WriteAllText(options.OutputFile, outputXml);
      });

      result.WithNotParsed(errors =>
      {
        foreach (var error in errors)
        {
          Console.WriteLine(error.Tag + ": " + error.ToString());
        }
        exitCode = 1;
      });

      return exitCode;
    }
  }
}
