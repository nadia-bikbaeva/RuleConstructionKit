namespace RuleConstructionKit.Domain;

public class Rule
{
    public int Id { get; set; }
    public int RoadmapId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Roadmap? Roadmap { get; set; }
    public Message? Message { get; set; }
    public ICollection<Profile> Profiles { get; set; } = new List<Profile>();
    public ICollection<TargetDocument> TargetDocuments { get; set; } = new List<TargetDocument>();

    public void SetRuleData(string name)
    {
        Name = name.Trim();
    }

    public void AddProfile(Profile profile)
    {
        Profiles.Add(profile);
    }

    public void AddTargetDocument(TargetDocument document)
    {
        TargetDocuments.Add(document);
    }
}