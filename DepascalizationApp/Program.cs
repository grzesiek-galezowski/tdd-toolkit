using System.IO;
using CommandLine;
using CommandLine.Text;

namespace DepascalizationApp
{
  public class Options
  {
    [Option('i', "input", Required = true, HelpText = "Input report file to be processed.")]
    public string InputFile { get; set; }

    [Option('o', "output", Required = true, HelpText = "Output report file name.")]
    public string OutputFile { get; set; }


    [ParserState]
    public IParserState LastParserState { get; set; }

    [HelpOption]
    public string GetUsage()
    {
      return HelpText.AutoBuild(this, helpText => HelpText.DefaultParsingErrorsHandler(this, helpText));
    }
  }

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
