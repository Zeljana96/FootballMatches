using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task5.Data;
using Task5.Dtos.Team;
using Task5.Models;
using Task5.Services.MatchService;

namespace Task5.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IMatchService _matchService;
        public TeamService(IMapper mapper, DataContext context, IMatchService matchService)
        {
            _matchService = matchService;
            _mapper = mapper;
            _context = context;

        }
        public async Task<IEnumerable<GetTeamDto>> GetAllTeams()
        {
            IEnumerable<Team> dbTeams = await _context.Teams.ToListAsync();
            IEnumerable<GetTeamDto> dbTeamsDtos = dbTeams.Select(t => _mapper.Map<GetTeamDto>(t));
            List<int> lista = new List<int>();
             foreach (var team in dbTeamsDtos)
            {
                List<Player> playerList =await _matchService.TeamPlayersById(team.TeamId);
                int numberOfPlayers = playerList.Count();
                team.NumberOfPlayers = numberOfPlayers;
                lista.Add(team.NumberOfPlayers);
            } 
            GetTeamDto nesto = new GetTeamDto();
            nesto = dbTeamsDtos.First(p => p.TeamId == 1);

            
            return dbTeamsDtos;
        }
        public async Task<List<int>> NumberOfPlayersByTeam()
        {
            List<int> result = new List<int>();
            IEnumerable<Team> dbTeams = await _context.Teams.ToListAsync();
            foreach(var team in dbTeams)
            {
                List<Player> playerList =await _matchService.TeamPlayersById(team.TeamId);
                int numberOfPlayers = playerList.Count();
                result.Add(numberOfPlayers);
            }
            return result;
        }
        public async Task<GetTeamDto> GetTeamById(int id)
        {
            Team dbTeam = await _context.Teams.FirstOrDefaultAsync(a => a.TeamId == id);
            List<Player> teamPlayers = await _context.Players.Where(p => p.Team.TeamId == id).ToListAsync();

            GetTeamDto teamForDisplaying = _mapper.Map<GetTeamDto>(dbTeam);
            teamForDisplaying.Players.Clear();
            foreach (var pl in teamPlayers.Select(p => p.Name))
            {
                teamForDisplaying.Players.Add(pl);
            }
            return (teamForDisplaying);
        }

        public async Task<ServiceResponse<Team>> addNewTeam(AddTeamDto newTeam)
        {
            ServiceResponse<Team> response = new ServiceResponse<Team>();
            Team teamForDb = _mapper.Map<Team>(newTeam);

            await _context.Teams.AddAsync(teamForDb);
            await _context.SaveChangesAsync();
            response.Data = await _context.Teams.FirstOrDefaultAsync(t => t.Name.ToLower().Equals(newTeam.Name.ToLower()));
            response.Message = "Team successfully added."; 
            return response;
        }

        public async Task<int> GetTeamIdByName(string teamName)
        {
            Team requiredTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Name.ToLower().Equals(teamName.ToLower()));
            int id = requiredTeam.TeamId;
            return id;
        }
    }
}