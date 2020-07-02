using System.Collections.Generic;

namespace DependabotConfigCreator.Interfaces
{
    interface IPackageEcosystem
    {
        IEnumerable<Package> GetEntries(string startDirectory, bool recursive);
    }
}