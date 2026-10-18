using RuleConstructionKit.Domain;

namespace RuleConstructionKit.Patterns.Builders;

public class ProfileBuilder
{
    public Profile CreateProfile(
        string fullName,
        int termDays,
        IEnumerable<(string Name, string Value)> properties)
    {
        var profile = new Profile();
        profile.SetProfileData(fullName, termDays);

        foreach (var property in properties)
        {
            var prop = new ProfileProperty();
            prop.SetProperty(property.Name, property.Value);
            profile.AddProperty(prop);
        }

        return profile;
    }
}