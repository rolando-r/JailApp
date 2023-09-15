using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("Persona");
        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.NombrePersona)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.ApellidoPersona)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.DireccionPersona)
        .IsRequired()
        .HasMaxLength(50);
    }
}