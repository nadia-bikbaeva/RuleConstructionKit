namespace RuleConstructionKit.Domain;

public class ProfileProperty
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public Profile? Profile { get; set; }

    public void SetProperty(string name, string value)
    {
        Name = name.Trim();
        Value = value.Trim();
    }
}