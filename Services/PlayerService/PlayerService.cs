using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task5.Data;
using Task5.Dtos.Player;
using Task5.Models;

namespace Task5.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PlayerService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<string> AddPlayerToDb(AddPlayerDto newPlayer)
        {
            ServiceResponse<List<Player>> response = new ServiceResponse<List<Player>>();
            Player playerForDb = _mapper.Map<Player>(newPlayer);
            playerForDb.Team = await _context.Teams.FirstOrDefaultAsync(t => t.TeamId == newPlayer.TeamId);
            await _context.Players.AddAsync(playerForDb);
            await _context.SaveChangesAsync();
        
            response.Data = await _context.Players.ToListAsync();
            response.Success = true;
            response.Message = "Player successfully added.";
            return playerForDb.Name;
        }

        public async Task<bool> DeletePlayer(string playerNameAndTeamId)
        {
            string[] separatedNameAndTeamId = playerNameAndTeamId.Split("+");
            try
            {

                Player player = await _context.Players.FirstOrDefaultAsync(p => p.Name.ToLower().Equals(separatedNameAndTeamId[0]));
                if (player != null)
                {
                    _context.Players.Remove(player);
                    await _context.SaveChangesAsync();
                    return true;
                    
                }
                else
                {
                    return false;
                }
            }catch(Exception ex){
                throw ex;
            }
            throw new System.NotImplementedException();
        }

        public async Task<bool> PlayerScored(string playerName, string matchId)
        {
            
            Player playerFromDb = await _context.Players.FirstOrDefaultAsync(p => p.Name.ToLower().Equals(playerName.ToLower()));
            playerFromDb.Goals = playerFromDb.Goals + 1;
            _context.Players.Update(playerFromDb);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}