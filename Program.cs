using System;
using System.IO;
using System.Linq;
using DependabotConfigCreator.Interfaces;

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

            var modules = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IPackageEcosystem).IsAssignableFrom(t) && t.IsClass).OrderBy(t => t.Name);

            var generator = new FileGenerator();
            foreach (var mod in modules)
            {
                var ecosystem = Activator.CreateInstance(mod) as IPackageEcosystem;
                var packages = ecosystem.GetEntries(args[0], true);

                foreach (var package in packages)
                {
                    generator.GenerateEntry(package);
                }
            }


            var outputFileName = Path.Combine(args[1], "dependabot.yml");
            generator.GenerateFile(outputFileName);

            Console.WriteLine($"File has been generated at {outputFileName}");
        }
    }
}
