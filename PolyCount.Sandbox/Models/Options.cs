using CommandLine;

namespace PolyCount.Sandbox.Models
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('f', "file", Required = true, HelpText = "Path to csv file.")]
        public string File { get; set; }
    }
}