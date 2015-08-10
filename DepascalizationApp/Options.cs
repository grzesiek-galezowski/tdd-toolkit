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
  }
}