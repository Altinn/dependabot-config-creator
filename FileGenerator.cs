using System;
using System.IO;
using System.Text;
using DependabotConfigCreator.Interfaces;

namespace DependabotConfigCreator
{
    class FileGenerator
    {
        private StringBuilder sb = new StringBuilder();

        public FileGenerator()
        {
            sb.AppendLine("version: 2");
            sb.AppendLine("updates:");
        }

        public void GenerateEntry(Package package)
        {
            if (package == null)
                throw new ArgumentNullException(nameof(package));

            if (string.IsNullOrEmpty(package.Directory))
                package.Directory = "/";

            sb.AppendLine($"  - package-ecosystem: {package.EcoSystem}");
            sb.AppendLine($"    directory: \"{package.Directory}\"");
            sb.AppendLine($"    schedule: ");
            sb.AppendLine($"      interval: {package.Interval}");
            sb.AppendLine($"      time: \"04:00\"");
            sb.AppendLine($"    open-pull-requests-limit: {package.PullRequestLimit}");

            if (package.ProductionOnly)
            {
                sb.AppendLine($"    allow:");
                sb.AppendLine($"      - dependency-type: \"production\"");
            }

        }

        public void GenerateFile(string fileName)
        {
            File.WriteAllText(fileName, sb.ToString());
        }
    }
}