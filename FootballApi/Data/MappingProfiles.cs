using FootballApi.Models;
using AutoMapper;
using FootballApi.Data.Models;

namespace FootballApi.Data
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Club
            CreateMap<Club, ClubDto>();
            CreateMap<ClubDto, Club>();

            CreateMap<ClubCreateDto, Club>();

            CreateMap<Club, ClubUpdateDto>();
            CreateMap<ClubUpdateDto, Club>();

            //Nationality
            CreateMap<Nationality, NationalityDto>();
            CreateMap<NationalityDto, Nationality>();

            CreateMap<NationalityCreateDto, Nationality>();

            CreateMap<Nationality, NationalityUpdateDto>();
            CreateMap<NationalityUpdateDto, Nationality>();
            //Player
            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerDto, Player>();

            CreateMap<PlayerCreateDto, Player>();

            CreateMap<Player, PlayerUpdateDto>();
            CreateMap<PlayerUpdateDto, Player>();
        }
    }
}
