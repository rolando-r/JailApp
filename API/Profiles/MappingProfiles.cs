using API.Dtos;
using AutoMapper;
using Dominio;
using Dominio.Entities;

namespace IncApi.Profiles;



public class MappingProfiles :Profile
{
    public MappingProfiles()
    {
/*         CreateMap<Area,AreaDto>()
        .ReverseMap()
        .ForMember(o => o.Lugares, d => d.Ignore()); */

        CreateMap<Pais,PaisxCiudadDto>().ReverseMap();

        CreateMap<Ciudad,CiudadDto>().ReverseMap();
        CreateMap<Pais,PaisDto>().ReverseMap()
        .ForMember(o => o.Ciudades, d => d.Ignore());
        CreateMap<Crimen,CrimenDto>().ReverseMap();
        CreateMap<Sede, SedeDto>().ReverseMap();
        CreateMap<Persona, PersonaDto>().ReverseMap();
    }
}