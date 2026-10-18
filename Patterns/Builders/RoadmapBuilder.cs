using RuleConstructionKit.Domain;

namespace RuleConstructionKit.Patterns.Builders;

public class RoadmapBuilder
{
    public Roadmap CreateRoadmap(string version, List<Rule> rules)
    {
        var roadmap = new Roadmap();
        roadmap.SetVersion(version);
        foreach (var rule in rules)
            roadmap.AddRule(rule);
        return roadmap;
    }
}