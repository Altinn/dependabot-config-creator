using System;
using System.IO;

namespace DependabotConfigCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length != 2 
                || !Directory.Exists(args[0]) 
                || !Directory.Exists(args[1]))
            {
                Console.WriteLine("Usage: ");
                Console.WriteLine("dotnet run {srcDirectory} {dstDirectory}");
                Console.WriteLine("  - srcDirectory - Where to start searching for projects ");
                Console.WriteLine("  - dstDirectory - Where to place the dependabot.yml file");

                Console.WriteLine("Both directories must exist.");
                return;
            }
                
            var scanner = new Scanner();
            var fileGenerator = new FileGenerator();

            var nugetFiles = scanner.GetCsProjects(args[0], true);
            var dockerFiles = scanner.GetDockerProjects(args[0], true);
            var mavenFiles = scanner.GetMavenProjects(args[0], true);
            var npmFiles = scanner.GetNpmProjects(args[0], true);


            foreach (var nuget in nugetFiles)
            {
                Console.WriteLine(nuget);
                fileGenerator.GenerateNugetEntry(nuget);
            }

            foreach (var docker in dockerFiles)
            {
                fileGenerator.GenerateDockerEntry(docker);
            }

            foreach (var npm in npmFiles)
            {
                fileGenerator.GenerateNpmEntry(npm);
            }

            foreach (var maven in mavenFiles)
            {
                fileGenerator.GenerateMavenEntry(maven);
            }

            var outputFileName = Path.Combine(args[1], "dependabot.yml");
            fileGenerator.GenerateFile(outputFileName);

            Console.WriteLine($"File has been generated at {outputFileName}");
        }
    }
}
