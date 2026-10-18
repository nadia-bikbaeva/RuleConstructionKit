namespace RuleConstructionKit.Domain;

public class Roadmap
{
    public int Id { get; set; }
    public string Version { get; set; } = string.Empty;
    public ICollection<Rule> Rules { get; set; } = new List<Rule>();

    public void SetVersion(string version)
    {
        Version = version.Trim();
    }

    public void AddRule(Rule rule)
    {
        Rules.Add(rule);
    }
}