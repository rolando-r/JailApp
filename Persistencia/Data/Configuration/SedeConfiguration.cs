using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class SedeConfiguration : IEntityTypeConfiguration<Sede>
{
    public void Configure(EntityTypeBuilder<Sede> builder)
    {
        builder.ToTable("sede");
        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.NombreSede)
        .IsRequired()
        .HasMaxLength(20);

        builder.HasOne(a => a.Ciudad)
        .WithMany(a => a.Sedes)
        .HasForeignKey(a => a.IdCiudad);
    }
}