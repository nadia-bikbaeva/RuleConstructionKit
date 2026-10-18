namespace RuleConstructionKit.Domain;

public class Profile
{
    public int Id { get; set; }
    public int RuleId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int TermDays { get; set; }
    public Rule? Rule { get; set; }
    public ICollection<ProfileProperty> Properties { get; set; } = new List<ProfileProperty>();

    public void SetProfileData(string fullName, int termDays)
    {
        FullName = fullName.Trim();
        TermDays = termDays;
    }

    public void AddProperty(ProfileProperty property)
    {
        Properties.Add(property);
    }
}