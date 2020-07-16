using CommandLine;

namespace DependabotConfigCreator
{
    class Options
    {
        [Option('s', "src", Required = true, HelpText = "The directory where the manifest files are located")]
        public string SourceDirectory { get; set; }

        [Option('r', "recursive", Default = true, Required = false, HelpText = "Recursive search for manifest files")]
        public bool Recursive { get; set; }

        [Option('d', "dst", Required = true, HelpText = "Where to write the dependabot.yml file")]
        public string TargetDirectory { get; set; }

        [Option('t', "exclude-tests", Default = false, Required = false, HelpText = "Exclude test projects")]
        public bool ExcludeTests { get; set; }
    }
}