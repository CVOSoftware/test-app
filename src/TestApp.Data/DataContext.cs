using Microsoft.EntityFrameworkCore;
using TestApp.Data.Entities;

namespace TestApp.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public DbSet<Patient> Patients { get; init; }
}