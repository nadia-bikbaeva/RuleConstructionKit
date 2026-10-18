namespace RuleConstructionKit.Domain;

public class Organization
{
    public int Id { get; set; }
    public int TargetDocumentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public TargetDocument? TargetDocument { get; set; }

    public void SetOrganizationData(string name, string address)
    {
        Name = name.Trim();
        Address = address.Trim();
    }
}