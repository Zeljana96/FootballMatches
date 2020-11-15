using System.Collections.Generic;
using System.Threading.Tasks;
using Task5.Dtos.Player;
using Task5.Models;

namespace Task5.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<string> AddPlayerToDb(AddPlayerDto newPlayer);

        Task<bool> DeletePlayer(string playerNameAndTeamId);
        Task<bool> PlayerScored(string playerName, string MatchId);
    }
}