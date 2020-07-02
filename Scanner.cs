using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DependabotConfigCreator
{
    /// <summary>
    /// Scan for files with a certain pattern.
    /// </summary>
    class Scanner
    {
        public static string[] DefaultIgnorePaths = new[] { @"\node_modules\", @"\bin\", "test" };
        public IEnumerable<string> GetCsProjects(string startDirectory, bool recursive)
        {
            return GetDirectoryWithFiles(startDirectory, "*.csproj", DefaultIgnorePaths, recursive);
        }

        public IEnumerable<string> GetDockerProjects(string startDirectory, bool recursive)
        {
            return GetDirectoryWithFiles(startDirectory, "Dockerfile", DefaultIgnorePaths, recursive);
        }

        public IEnumerable<string> GetMavenProjects(string startDirectory, bool recursive)
        {
            return GetDirectoryWithFiles(startDirectory, "pom.xml", DefaultIgnorePaths, recursive);
        }


        public IEnumerable<string> GetNpmProjects(string startDirectory, bool recursive)
        {
            return GetDirectoryWithFiles(startDirectory, "package.json", DefaultIgnorePaths, recursive);
        }


        public IEnumerable<string> GetDirectoryWithFiles(string startDirectory, string filePattern, string[] ignorePaths, bool recursive)
        {
            return System.IO.Directory.GetFiles(startDirectory, filePattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                .Where(f => (ignorePaths?.Any() == false || (false == ignorePaths.Any(ip => f.Contains(ip, StringComparison.OrdinalIgnoreCase)))))
                .Select(f => Path.GetDirectoryName(f).Replace(startDirectory, "").Replace("\\", "/")).Distinct();
        }
    }
}