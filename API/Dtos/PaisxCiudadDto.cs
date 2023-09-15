namespace API.Dtos;
public class PaisxCiudadDto
{
    public int Id { get; set;}
    public string NombrePais { get; set;}
    public ICollection<CiudadDto> Ciudades { get; set; }
}