namespace Dominio.Entities;
public class Sede : BaseEntity
{
    public string NombreSede { get; set; }
    public int IdCiudad { get; set; }
    public Ciudad Ciudad { get; set; }
}