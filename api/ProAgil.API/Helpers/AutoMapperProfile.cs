using System.Linq;
using AutoMapper;
using ProAgil.API.DTOs;
using ProAgil.Domain.Entities;

namespace ProAgil.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Evento, EventoDTO>()
                        .ForMember(dest => dest.Palestrantes, opt => {
                            opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Palestrante).ToList());
                        });
            CreateMap<Palestrante, PalestranteDTO>()
                        .ForMember( dest =>  dest.Eventos, opt => {
                            opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Evento).ToList());
                        });
            CreateMap<RedeSocial, RedeSocialDTO>();
            CreateMap<Lote, LoteDTO>();

        }
    }
}