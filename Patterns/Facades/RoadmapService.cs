using RuleConstructionKit.Domain;
using RuleConstructionKit.Patterns.Builders;
namespace RuleConstructionKit.Patterns.Facades;

public class RoadmapService
{
    public Roadmap CreateRoadmapWithRules(string version, List<Rule> rules)
    {
        var builder = new RoadmapBuilder();
        return builder.CreateRoadmap(version, rules);
    }
}