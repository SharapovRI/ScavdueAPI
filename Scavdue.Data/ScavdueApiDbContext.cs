using Microsoft.EntityFrameworkCore;
using Scavdue.Core.Models;
using Scavdue.Data.ModelsConfigurations;

namespace Scavdue.Data;

public partial class ScavdueApiDbContext : DbContext
{
    public ScavdueApiDbContext()
    {
    }

    public ScavdueApiDbContext(DbContextOptions<ScavdueApiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdministrativeUnit> AdministrativeUnits { get; set; }

    public virtual DbSet<AdministrativeUnitPolygon> AdministrativeUnitPolygons { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Population> Populations { get; set; }

    public virtual DbSet<UnitObject> UnitObjects { get; set; }

    public virtual DbSet<UnitObjectClass> UnitObjectClasses { get; set; }

    public virtual DbSet<UnitObjectPolygon> UnitObjectPolygons { get; set; }

    public virtual DbSet<UnitObjectType> UnitObjectTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ScavdueAPI_DB;Username=postgres;Password=PostgreSQL");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdministrativeUnitConfiguration());
        modelBuilder.ApplyConfiguration(new AdministrativeUnitPolygonConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new PopulationConfiguration());
        modelBuilder.ApplyConfiguration(new UnitObjectConfiguration());
        modelBuilder.ApplyConfiguration(new UnitObjectClassConfiguration());
        modelBuilder.ApplyConfiguration(new UnitObjectPolygonConfiguration());
        modelBuilder.ApplyConfiguration(new UnitObjectTypeConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
