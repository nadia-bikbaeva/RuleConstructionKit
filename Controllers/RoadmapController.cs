using RuleConstructionKit.Data;
using RuleConstructionKit.Domain;
using RuleConstructionKit.Patterns.Facades;
using RuleConstructionKit.Patterns.Repositories;
using RuleConstructionKit.Patterns.Builders;
namespace RuleConstructionKit.Controllers;

public class RoadmapController
{
    private readonly RoadmapService _roadmapService;
    private readonly RoadmapRepository _storage;

    public RoadmapController(RulesDbContext db)
        : this(new RoadmapService(), new RoadmapRepository(db))
    {
    }

    public RoadmapController(RoadmapService roadmapService, RoadmapRepository storage)
    {
        _roadmapService = roadmapService;
        _storage = storage;
    }

    public Roadmap? CreateRoadmapWithRules(
        string version,
        List<(string RuleName, string DocumentName, string OrganizationName, string OrganizationAddress, int TermDays, string FullName, List<(string Name, string Value)> Properties)> rulesData)
    {
        var rules = new List<Rule>();
        var ruleBuilder = new RuleBuilder();

        foreach (var data in rulesData)
        {
            var rule = ruleBuilder.CreateRule(
                data.RuleName,
                data.DocumentName,
                data.OrganizationName,
                data.OrganizationAddress,
                data.TermDays,
                data.FullName,
                data.Properties);
            rules.Add(rule);
        }

        var roadmap = _roadmapService.CreateRoadmapWithRules(version, rules);
        _storage.SaveRoadmapWithTransaction(roadmap);
        return _storage.GetRoadmapWithDetails(roadmap.Id);
    }
}