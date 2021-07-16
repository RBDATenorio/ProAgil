using AutoMapper;
using ProAgil.API.DTOs;
using ProAgil.Domain.Entities;

namespace ProAgil.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Evento, EventoDTO>();
            CreateMap<Palestrante, PalestranteDTO>();
            CreateMap<RedeSocial, RedeSocialDTO>();
            CreateMap<Lote, LoteDTO>();

        }
    }
}