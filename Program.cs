using Microsoft.EntityFrameworkCore;
using RuleConstructionKit.Controllers;
using RuleConstructionKit.Data;

var connectionString = Environment.GetEnvironmentVariable("BD_GROUP")
    ?? "Host=localhost;Port=5432;Database=postgres_6_group;Username=postgres;Password=12345";

var options = new DbContextOptionsBuilder<RulesDbContext>()
    .UseNpgsql(connectionString)
    .Options;

using var db = new RulesDbContext(options);
db.Database.EnsureCreated();

var controller = new RoadmapController(db);

var rulesData = new List<(string RuleName, string DocumentName, string OrganizationName, string OrganizationAddress, int TermDays, string FullName, List<(string Name, string Value)> Properties)>
{
    (
        RuleName: "Результат медицинского освидетельствования",
        DocumentName: "Справка об отсутствии наркомании и инфекционных заболеваний",
        OrganizationName: "Медицинская организация",
        OrganizationAddress: "по месту пребывания",
        TermDays: 30,
        FullName: "Иванов Иван Иванович",
        Properties: new List<(string, string)> { ("Цель въезда", "Трудовая деятельность"), ("Требование", "Отсутствие заболеваний") }
    ),
    (
        RuleName: "Патент",
        DocumentName: "Патент на работу",
        OrganizationName: "УМВД России / ФГУП «ПВС» МВД России",
        OrganizationAddress: "по месту пребывания",
        TermDays: 30,
        FullName: "Иванов Иван Иванович",
        Properties: new List<(string, string)> { ("Основание", "Трудовая деятельность"), ("Гражданство", "Азербайджан, Таджикистан, Узбекистан, Молдова, Украина, Киргизия, Казахстан, Армения") }
    )
};

var roadmap = controller.CreateRoadmapWithRules(
    version: $"release-{DateTime.Now:yyyyMMdd-HHmmss-fff}",
    rulesData: rulesData);

if (roadmap == null)
{
    Console.WriteLine("Ошибка: дорожная карта не создана");
    return;
}


Console.WriteLine($"Дорожная карта: Id={roadmap.Id}, Версия={roadmap.Version}");
Console.WriteLine($"Всего правил: {roadmap.Rules.Count}");

foreach (var rule in roadmap.Rules)
{
    var profile = rule.Profiles.First();
    var doc = rule.TargetDocuments.First();
    var org = doc.Organizations.First();
    Console.WriteLine($"\nПравило: {rule.Name}");
    Console.WriteLine($"  Профиль: {profile.FullName}, срок {profile.TermDays} дней");
    foreach (var prop in profile.Properties)
        Console.WriteLine($"    {prop.Name} = {prop.Value}");
    Console.WriteLine($"  Документ: {doc.Name}");
    Console.WriteLine($"  Организация: {org.Name}, {org.Address}");
    Console.WriteLine($"  Сообщение: {rule.Message?.Description} / {rule.Message?.Rejection}");
}