using AutoMapper;
using MINIMAL_API.Domains;
using RangoAgil.API.Entities;

namespace RangoAgil.API.Profiles;

public class RangoAgilProfile : Profile
{
    public RangoAgilProfile()
    {
        CreateMap<Rango, RangoDTO>().ReverseMap();
        CreateMap<Rango, RangoParaCriacaoDTO>().ReverseMap();
        CreateMap<Rango, RangoParaAtualizacaoDTO>().ReverseMap();
        CreateMap<Ingrediente, IngredienteDTO>()
            .ForMember(
                d => d.RangoId,
                o => o.MapFrom(s => s.Rangos.First().Id)
            );
    }
}