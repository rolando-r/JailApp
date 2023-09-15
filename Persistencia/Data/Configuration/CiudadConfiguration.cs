using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        builder.ToTable("Ciudad");
        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.NombreCiudad)
        .IsRequired()
        .HasMaxLength(20);

        builder.HasOne(a => a.Pais)
        .WithMany(a => a.Ciudades)
        .HasForeignKey(a => a.IdPais);
    }
}