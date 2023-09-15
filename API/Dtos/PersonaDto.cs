namespace API.Dtos;
public class PersonaDto
{
    public int Id { get; set;}
    public string NombrePersona { get; set; }
    public string ApellidoPersona { get; set; }
    public string DireccionPersona { get; set; }
    public ICollection<CrimenDto> Crimenes { get; set; }
}