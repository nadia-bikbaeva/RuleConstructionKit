namespace RuleConstructionKit.Domain;

public class Message
{
    public int Id { get; set; }
    public int RuleId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Rejection { get; set; } = string.Empty;

    public Rule? Rule { get; set; }

    public void SetMessageData(string description, string rejection)
    {
        Description = description.Trim();
        Rejection = rejection.Trim();
    }
}