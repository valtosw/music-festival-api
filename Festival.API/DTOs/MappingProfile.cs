using AutoMapper;
using MusicFestival.Core.Entities;

namespace MusicFestival.API.DTOs
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Artist, ArtistDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Festival, FestivalDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Performance, PerformanceDto>().ReverseMap();
            CreateMap<Stage, StageDto>().ReverseMap();
            CreateMap<Ticket, TicketDto>().ReverseMap();
        }
    }
}
