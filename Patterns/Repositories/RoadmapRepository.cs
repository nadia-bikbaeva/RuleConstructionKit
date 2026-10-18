using Microsoft.EntityFrameworkCore;
using RuleConstructionKit.Data;
using RuleConstructionKit.Domain;

namespace RuleConstructionKit.Patterns.Repositories;

public class RoadmapRepository
{
    private readonly RulesDbContext _db;

    public RoadmapRepository(RulesDbContext db)
    {
        _db = db;
    }

    public int SaveRoadmapWithTransaction(Roadmap roadmap)
    {
        using var transaction = _db.Database.BeginTransaction();
        try
        {
            _db.Roadmaps.Add(roadmap);
            var result = _db.SaveChanges();
            transaction.Commit();
            return result;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public Roadmap? GetRoadmapWithDetails(int roadmapId)
    {
        return _db.Roadmaps
            .Include(r => r.Rules)
                .ThenInclude(rule => rule.Message)
            .Include(r => r.Rules)
                .ThenInclude(rule => rule.Profiles)
                .ThenInclude(profile => profile.Properties)
            .Include(r => r.Rules)
                .ThenInclude(rule => rule.TargetDocuments)
                .ThenInclude(doc => doc.Organizations)
            .FirstOrDefault(r => r.Id == roadmapId);
    }
}