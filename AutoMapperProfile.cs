using AutoMapper;
using Task5.Dtos.Match;
using Task5.Dtos.Player;
using Task5.Dtos.Team;
using Task5.Models;

namespace Task5
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Team, GetTeamDto>();
            CreateMap<AddTeamDto, Team>();
            CreateMap<Match, GetMatchDto>();
            CreateMap<AddPlayerDto, Player>();
            CreateMap<Match, GetMatchRestDto>();
        }
    }
}