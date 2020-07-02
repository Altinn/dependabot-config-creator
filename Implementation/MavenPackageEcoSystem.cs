using System.Collections.Generic;
using DependabotConfigCreator.Interfaces;

namespace DependabotConfigCreator.Implementation
{
    public class MavenPackageEcoSystem : IPackageEcosystem
    {
        public const string EcoSystem = "maven";
        public IEnumerable<Package> GetEntries(string startDirectory, bool recursive)
        {
            var dirs = Scanner.GetDirectoryWithFiles(startDirectory, "pom.xml", Scanner.DefaultIgnorePaths, recursive);
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