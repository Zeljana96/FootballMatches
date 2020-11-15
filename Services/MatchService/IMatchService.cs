using System.Collections.Generic;
using System.Threading.Tasks;
using Task5.Dtos.Match;
using Task5.Dtos.Team;
using Task5.Models;

namespace Task5.Services.MatchService
{
    public interface IMatchService
    {
        Task<IEnumerable<GetMatchDto>> GetAllMatches();
        Task<List<Player>> TeamPlayersById(int TeamId);
        Task<List<string>> TeamStatistic();
        Task<IEnumerable<Player>> PlayerStatistic();

        Task<ServiceResponse<List<GetMatchDto>>> AddMatch(AddMatchDto newMatch);
        Task<GetMatchDto> GetMatchById(int id);

        Task<bool> Goal(string matchId, string teamName);

        Task<ServiceResponse<List<GetMatchRestDto>>> GetAllMatchesRest();
    }
}