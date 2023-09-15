using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class CrimenConfiguration : IEntityTypeConfiguration<Crimen>
{
    public void Configure(EntityTypeBuilder<Crimen> builder)
    {
        builder.ToTable("Crimen");
        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.DescricionCrimen)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasOne(a => a.Persona)
        .WithMany(a => a.Crimenes)
        .HasForeignKey(a => a.IdPersona);

        builder.HasOne(a => a.Ciudad)
        .WithMany(a => a.Crimenes)
        .HasForeignKey(a => a.IdCiudad);
    }
}