using System;
using System.IO;
using System.Text;

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

        public void GenerateDockerEntry(string directoryName)
        {
            GenerateEntry("docker", directoryName);
        }

        public void GenerateEntry(string provider, string directoryName, bool productionOnly = false)
        {
            if (string.IsNullOrEmpty(directoryName))
                directoryName = "/";
                
            sb.AppendLine($"  - package-ecosystem: {provider}");
            sb.AppendLine($"    directory: \"{directoryName}\"");
            sb.AppendLine($"    schedule: ");
            sb.AppendLine($"      interval: daily");
            sb.AppendLine($"      time: \"04:00\"");
            sb.AppendLine($"    open-pull-requests-limit: 40");
            
            if (productionOnly)
            {
                sb.AppendLine($"    allow:");
                sb.AppendLine($"      - dependency-type: \"production\"");
            }

        }

        public void GenerateNpmEntry(string directoryName)
        {
            GenerateEntry("npm", directoryName, productionOnly: true);
        }

        public void GenerateMavenEntry(string directoryName)
        {
            GenerateEntry("maven", directoryName);
        }

        public void GenerateNugetEntry(string directoryName)
        {
            GenerateEntry("nuget", directoryName);
        }

        public void GenerateFile(string fileName)
        {
            File.WriteAllText(fileName, sb.ToString());
        }
    }
}