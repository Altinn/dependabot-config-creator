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
        private static string[] _defaultIgnorePathWithTest = new[] { @"\node_modules\", @"\bin\", "test" };
        private static string[] _defaultIgnorePathsWithoutTest = new[] { @"\node_modules\", @"\bin\" };
        public static string[] DefaultIgnorePaths => ExcludeTests ?   _defaultIgnorePathWithTest : _defaultIgnorePathsWithoutTest;
                                            
        public static bool ExcludeTests { get; set;} = false;
            
        
        public static IEnumerable<string> GetDirectoryWithFiles(string startDirectory, string filePattern, string[] ignorePaths, bool recursive)
        {
            return System.IO.Directory.GetFiles(startDirectory, filePattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                .Where(f => (ignorePaths?.Any() == false || (false == ignorePaths.Any(ip => f.Contains(ip, StringComparison.OrdinalIgnoreCase)))))
                .Select(f => Path.GetDirectoryName(f).Replace(startDirectory, "").Replace("\\", "/")).Distinct();
        }
    }
}