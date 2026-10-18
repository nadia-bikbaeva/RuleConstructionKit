using RuleConstructionKit.Domain;

namespace RuleConstructionKit.Patterns.Builders;

public class RuleBuilder
{
    public Rule CreateRule(
        string ruleName,
        string documentName,
        string organizationName,
        string organizationAddress,
        int termDays,
        string fullName,
        IEnumerable<(string Name, string Value)>? properties = null,
        string messageDescription = "Рекомендация отсутствует",
        string messageRejection = "Причина отказа не указана")
    {
        var rule = new Rule();
        rule.SetRuleData(ruleName);

        var document = new TargetDocument();
        document.SetName(documentName);

        var organization = new Organization();
        organization.SetOrganizationData(organizationName, organizationAddress);
        document.AddOrganization(organization);
        rule.AddTargetDocument(document);

        var profileBuilder = new ProfileBuilder();
        var profile = profileBuilder.CreateProfile(
            fullName,
            termDays,
            properties ?? new List<(string, string)>());
        rule.AddProfile(profile);

        var message = new Message();
        message.SetMessageData(messageDescription, messageRejection);
        rule.Message = message;

        return rule;
    }
}