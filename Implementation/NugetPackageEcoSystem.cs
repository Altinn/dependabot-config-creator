using System.Collections.Generic;
using DependabotConfigCreator.Interfaces;
using DependabotConfigCreator;

namespace DependabotConfigCreator.Implementation
{
    public class NugetPackageEcoSystem : IPackageEcosystem
    {
        public const string EcoSystem = "nuget";

        public IEnumerable<Package> GetEntries(string startDirectory, bool recursive)
        {

            var dirs = Scanner.GetDirectoryWithFiles(startDirectory, "*.csproj", Scanner.DefaultIgnorePaths, recursive);
            foreach (var dir in dirs)
            {
                yield return new Package
                {
                    EcoSystem = EcoSystem,
                    Directory = dir,
                    Interval = "daily"
                };
            }
        }
    }
}