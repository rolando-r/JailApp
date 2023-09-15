namespace Dominio.Entities;
public class Crimen : BaseEntity
{
    public string DescricionCrimen { get; set; }
    public int IdPersona { get; set; }
    public Persona Persona { get; set; }
    public int IdCiudad { get; set; }
    public Ciudad Ciudad { get; set; }

}