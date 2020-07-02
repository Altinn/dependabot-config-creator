using System.Collections.Generic;
using DependabotConfigCreator.Interfaces;

namespace DependabotConfigCreator.Implementation
{
    public class NpmPackageEcoSystem : IPackageEcosystem
    {
        public const string EcoSystem = "npm";
        public IEnumerable<Package> GetEntries(string startDirectory, bool recursive)
        {
            var dirs = Scanner.GetDirectoryWithFiles(startDirectory, "package.json", Scanner.DefaultIgnorePaths, recursive);
            foreach (var dir in dirs)
            {
                yield return new Package
                {
                    EcoSystem = EcoSystem,
                    Directory = dir,
                    Interval = "daily",
                    ProductionOnly = true
                };
            }
        }
    }
}