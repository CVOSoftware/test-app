using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.Data.Entities;

namespace TestApp.Data.Configurations;

internal sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Use)
            .HasMaxLength(300)
            .IsRequired(false);

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Family)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Patronymic)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Gender)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .IsRequired();

        builder.HasIndex(p => p.DoB);

        builder.Property(p => p.DoB)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .IsRequired();
    }
}