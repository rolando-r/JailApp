namespace Dominio.Entities;
public class Persona : BaseEntity
{
    public string NombrePersona { get; set; }
    public string ApellidoPersona { get; set; }
    public string DireccionPersona { get; set; }
    public ICollection<Crimen> Crimenes { get; set; }
}