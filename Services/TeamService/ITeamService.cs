using System.Collections.Generic;
using System.Threading.Tasks;
using Task5.Dtos.Team;
using Task5.Models;

namespace Task5.Services.TeamService
{
    public interface ITeamService
    {
         Task<IEnumerable<GetTeamDto>> GetAllTeams();

         Task<GetTeamDto> GetTeamById(int id);
         
         Task<List<int>> NumberOfPlayersByTeam();

         Task<ServiceResponse<Team>> addNewTeam(AddTeamDto newTeam);

         Task<int> GetTeamIdByName(string teamName);
         
    }
}