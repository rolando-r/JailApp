namespace API.Dtos;
public class CiudadDto
{
    public int Id { get; set;}
    public string NombreCiudad { get; set; }
    public ICollection<SedeDto> Sedes { get; set;}
    public ICollection<CrimenDto> Crimenes { get; set; }
}