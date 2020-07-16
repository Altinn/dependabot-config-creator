using System;
using System.IO;
using System.Linq;
using CommandLine;
using DependabotConfigCreator.Interfaces;

namespace DependabotConfigCreator
{
    class Program
    {
        static void Main(string[] args)
        {           
            int result = CommandLine.Parser.Default.ParseArguments<Options>(args)
                .MapResult(
                    (Options o) => RunOptions(o),
                    err => 1
                );

        }

        static int RunOptions(Options o)
        {
            if (!Directory.Exists(o.SourceDirectory) || !Directory.Exists(o.TargetDirectory))
            {
                Console.WriteLine("Directories must exist");

                return 1;
            }

            Scanner.ExcludeTests = o.ExcludeTests;

            var modules = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IPackageEcosystem).IsAssignableFrom(t) && t.IsClass).OrderBy(t => t.Name);
            
            var generator = new FileGenerator();
            foreach (var mod in modules)
            {
                var ecosystem = Activator.CreateInstance(mod) as IPackageEcosystem;
                var packages = ecosystem.GetEntries(o.SourceDirectory, o.Recursive);

                foreach (var package in packages)
                {
                    generator.GenerateEntry(package);
                }
            }


            var outputFileName = Path.Combine(o.TargetDirectory, "dependabot.yml");
            generator.GenerateFile(outputFileName);

            Console.WriteLine($"File has been generated at {outputFileName}");


            return 0;
        }
    }
}
