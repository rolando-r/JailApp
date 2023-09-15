namespace Dominio.Entities;
public class Ciudad : BaseEntity
{
    public string NombreCiudad { get; set; }
    public int IdPais { get; set; }
    public Pais Pais { get; set; }
    public ICollection<Sede> Sedes { get; set;}
    public ICollection<Crimen> Crimenes { get; set; }
}