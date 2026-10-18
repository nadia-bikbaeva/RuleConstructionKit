using Microsoft.EntityFrameworkCore;
using RuleConstructionKit.Domain;

namespace RuleConstructionKit.Data;

public class RulesDbContext : DbContext
{
    public RulesDbContext(DbContextOptions<RulesDbContext> options) : base(options) { }
    public DbSet<Roadmap> Roadmaps => Set<Roadmap>();
    public DbSet<Rule> Rules => Set<Rule>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<ProfileProperty> ProfileProperties => Set<ProfileProperty>();
    public DbSet<TargetDocument> TargetDocuments => Set<TargetDocument>();
    public DbSet<Organization> Organizations => Set<Organization>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Roadmap>(entity =>
        {
            entity.ToTable("roadmaps");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Version).HasMaxLength(40).IsRequired();
            entity.HasIndex(x => x.Version).IsUnique();
            entity.HasMany(x => x.Rules).WithOne(x => x.Roadmap).HasForeignKey(x => x.RoadmapId);
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.ToTable("rules");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.HasMany(x => x.Profiles).WithOne(x => x.Rule).HasForeignKey(x => x.RuleId);
            entity.HasMany(x => x.TargetDocuments).WithOne(x => x.Rule).HasForeignKey(x => x.RuleId);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("messages");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Description).HasMaxLength(2000).IsRequired();
            entity.Property(x => x.Rejection).HasMaxLength(2000);
            entity.HasOne(x => x.Rule)
                  .WithOne(x => x.Message)
                  .HasForeignKey<Message>(x => x.RuleId);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.ToTable("profiles");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FullName).HasMaxLength(250).IsRequired();
            entity.Property(x => x.TermDays).IsRequired();
            entity.HasMany(x => x.Properties).WithOne(x => x.Profile).HasForeignKey(x => x.ProfileId);
        });

        modelBuilder.Entity<ProfileProperty>(entity =>
        {
            entity.ToTable("profile_properties");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Value).HasMaxLength(500).IsRequired();
        });

        modelBuilder.Entity<TargetDocument>(entity =>
        {
            entity.ToTable("target_documents");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(500).IsRequired();
            entity.HasMany(x => x.Organizations).WithOne(x => x.TargetDocument).HasForeignKey(x => x.TargetDocumentId);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.ToTable("organizations");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(250).IsRequired();
            entity.Property(x => x.Address).HasMaxLength(500).IsRequired();
        });
    }
}