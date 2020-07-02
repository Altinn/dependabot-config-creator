namespace DependabotConfigCreator.Interfaces
{
    public class Package
    {
        public string EcoSystem { get; set; }
        public string Directory { get; set; }
        public string Interval { get; set; }
        public bool ProductionOnly { get; set; }

        public int PullRequestLimit { get; set; } = 40;
    }
}