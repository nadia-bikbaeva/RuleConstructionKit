namespace RuleConstructionKit.Domain;

public class TargetDocument
{
    public int Id { get; set; }
    public int RuleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Rule? Rule { get; set; }
    public ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    public void SetName(string name)
    {
        Name = name.Trim();
    }

    public void AddOrganization(Organization organization)
    {
        Organizations.Add(organization);
    }
}