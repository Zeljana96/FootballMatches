using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task5.Data;
using Task5.Dtos.Match;
using Task5.Dtos.Team;
using Task5.Models;

namespace Task5.Services.MatchService
{
    public class MatchService : IMatchService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MatchService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        // we use this function in creating new match, to check that desired date for match is available.
        private bool checkTeamDates(string teamName, string newMatchDate)
        {
            IEnumerable<Match> dbMatches = _context.Matches.ToList();
            List<string> lista = new List<string>();
            foreach (var match in dbMatches)
            {
                string matchDate = match.MatchTime.ToString("MM/dd/yyyy");

                if ((teamName.Equals(match.HostTeam) || teamName.Equals(match.GuestTeam)) && matchDate.Equals(newMatchDate))
                {
                    return true;
                }
            }
            return false;
        }
        // we use this function in match creating process, to check that desired players are available.
        private string checkChosenPlayers(string teamName, List<string> playersList)
        {
            List<string> playersOfRequiredTeam = TeamPlayersByName(teamName);
            foreach (var player in playersList)
            {
                if (!playersOfRequiredTeam.Contains(player))
                {
                    return player;
                }
            }

            return null;

        }
        public async Task<ServiceResponse<List<GetMatchDto>>> AddMatch(AddMatchDto newMatch)
        {
            ServiceResponse<List<GetMatchDto>> response = new ServiceResponse<List<GetMatchDto>>();
            string newMatchDate = newMatch.MatchTime.ToString("MM/dd/yyyy");
            if (newMatch.HostTeam.ToLower().Equals(newMatch.GuestTeam.ToLower()))
            {
                response.Success = false;
                response.Message = "Guest team and host team can't be the same.";
            }
            else if (DateTime.Compare(DateTime.UtcNow, newMatch.MatchTime) > 0)
            {
                response.Success = false;
                response.Message = "Start time of the match can't be in past.";
            }
            else if (newMatch.HostTeamPlayers.Count() < 6)
            {
                response.Success = false;
                response.Message = "You must pick at least 6 players from host team.";
            }
            else if (newMatch.GuestTeamPlayers.Count() < 6)
            {
                response.Success = false;
                response.Message = "You must pick at least 6 players from guest team.";
            }
            else if (checkTeamDates(newMatch.HostTeam, newMatchDate) || checkTeamDates(newMatch.GuestTeam, newMatchDate))
            {
                response.Success = false;
                response.Message = "Teams can't have two mathes at the same day.";

            }
            else if (checkChosenPlayers(newMatch.HostTeam, newMatch.HostTeamPlayers) != null)
            {
                response.Success = false;
                response.Message = $"Player {checkChosenPlayers(newMatch.HostTeam, newMatch.HostTeamPlayers)} doesn't play in {newMatch.HostTeam}.";
            }
            else if (checkChosenPlayers(newMatch.GuestTeam, newMatch.GuestTeamPlayers) != null)
            {
                response.Success = false;
                response.Message = $"Player {checkChosenPlayers(newMatch.GuestTeam, newMatch.GuestTeamPlayers)} doesn't play in {newMatch.GuestTeam}.";
            }
            else
            {
                Match newMatchDb = new Match();
                newMatchDb.HostTeam = newMatch.HostTeam;
                newMatchDb.GuestTeam = newMatch.GuestTeam;
                newMatchDb.MatchTime = newMatch.MatchTime;
                newMatchDb.MatchPlace = newMatch.MatchPlace;
                newMatchDb.Result = "0:0";

                await _context.Matches.AddAsync(newMatchDb);
                await _context.SaveChangesAsync();

                foreach (var guestTeamPlayer in newMatch.GuestTeamPlayers)
                {
                    Player playerDb1 = await _context.Players.FirstOrDefaultAsync(p => p.Name.Equals(guestTeamPlayer));
                    playerDb1.Matches = playerDb1.Matches + 1;

                    _context.Players.Update(playerDb1);
                    await _context.SaveChangesAsync();
                }
                foreach (var hostTeamPlayer in newMatch.HostTeamPlayers)
                {
                    Player playerDb2 = await _context.Players.FirstOrDefaultAsync(p => p.Name.ToLower().Equals(hostTeamPlayer.ToLower()));
                    playerDb2.Matches = playerDb2.Matches + 1;

                    _context.Players.Update(playerDb2);
                    await _context.SaveChangesAsync();
                }
                response.Data = (_context.Matches.Select(a => _mapper.Map<GetMatchDto>(a))).ToList();
            }
            return response;
        }

        public async Task<IEnumerable<GetMatchDto>> GetAllMatches()
        {
            IEnumerable<Match> dbMatches = await _context.Matches.ToListAsync();
            IEnumerable<GetMatchDto> dbMatchDtos = dbMatches.Select(t => _mapper.Map<GetMatchDto>(t));

            return dbMatchDtos;
        }

        public async Task<IEnumerable<Player>> PlayerStatistic()
        {
            IEnumerable<Player> playerStatistic = await _context.Players.ToListAsync();
            return playerStatistic;
        }

        public async Task<List<Player>> TeamPlayersById(int TeamId)
        {
            List<Player> teamPlayers = await _context.Players.Where(p => p.Team.TeamId == TeamId).ToListAsync();
            return teamPlayers;
        }
        private List<string> TeamPlayersByName(string TeamName)
        {
            List<Player> teamPlayers = _context.Players.Where(p => p.Team.Name.ToLower().Equals(TeamName.ToLower())).ToList();
            List<string> teamPlayersName = teamPlayers.Select(p => p.Name).ToList();
            return teamPlayersName;
        }

        public async Task<List<string>> TeamStatistic()
        {
            IEnumerable<Team> dbTeams = await _context.Teams.ToListAsync();
            IEnumerable<Match> dbMatches = await _context.Matches.ToListAsync();
            List<string> teamsAndStatistics = new List<string>();
            foreach (var team in dbTeams)
            {
                int numberOfWins = 0;
                int numberOfDraws = 0;
                int numberOfLoses = 0;
                foreach (var match in dbMatches)
                {
                    if (team.Name.ToLower() == match.HostTeam.ToLower())
                    {
                        if (int.Parse(match.Result.Split(":")[0]) > int.Parse(match.Result.Split(":")[1]))
                        {
                            numberOfWins++;
                        }
                        else if (int.Parse(match.Result.Split(":")[0]) == int.Parse(match.Result.Split(":")[1]))
                        {
                            numberOfDraws++;
                        }
                        else
                        {
                            numberOfLoses++;
                        }
                    }
                    if (team.Name.ToLower() == match.GuestTeam.ToLower())
                    {
                        if (int.Parse(match.Result.Split(":")[0]) > int.Parse(match.Result.Split(":")[1]))
                        {
                            numberOfLoses++;
                        }
                        else if (int.Parse(match.Result.Split(":")[0]) == int.Parse(match.Result.Split(":")[1]))
                        {
                            numberOfDraws++;
                        }
                        else
                        {
                            numberOfWins++;
                        }
                    }

                }
                string joined = team.Name + "/" + numberOfWins + "/" + numberOfDraws + "/" + numberOfLoses;
                teamsAndStatistics.Add(joined);

            }
            return teamsAndStatistics;
        }
        public async Task<GetMatchDto> GetMatchById(int id)
        {
            Match dbMatch = await _context.Matches.FirstOrDefaultAsync(a => a.MatchId == id);

            GetMatchDto matchForDisplaying = _mapper.Map<GetMatchDto>(dbMatch);

            return (matchForDisplaying);
        }

        public async Task<bool> Goal(string matchId, string teamName)
        {
            int matchIdParsed = int.Parse(matchId);
            Match matchFromDb = await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == matchIdParsed);
            if (matchFromDb.HostTeam.ToLower().Equals(teamName.ToLower()))
            {
                int hostGoals = int.Parse(matchFromDb.Result.Split(":")[0]);
                hostGoals++;
                matchFromDb.Result = $"{hostGoals}:{matchFromDb.Result.Split(":")[1]}";
                _context.Matches.Update(matchFromDb);
                await _context.SaveChangesAsync();
            }
            else
            {
                int guestGoals = int.Parse(matchFromDb.Result.Split(":")[1]);
                guestGoals++;
                matchFromDb.Result = $"{matchFromDb.Result.Split(":")[0]}:{guestGoals}";
                _context.Matches.Update(matchFromDb);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<ServiceResponse<List<GetMatchRestDto>>> GetAllMatchesRest()
        {
            ServiceResponse<List<GetMatchRestDto>> response = new ServiceResponse<List<GetMatchRestDto>>();
            List<Match> matchesFromDb = await _context.Matches.ToListAsync();
            List<GetMatchRestDto> result = (matchesFromDb.Select(m => _mapper.Map<GetMatchRestDto>(m))).ToList();

            List<PlayerGoalMatch> infoFromDb = await _context.PlayerGoalMatches.ToListAsync();
            

            foreach (var match in result)
            {
                List<string> scorersOfGuestTeam = new List<string>();
                List<string> scorersOfHostTeam = new List<string>();
                match.HostTeamPlayers = TeamPlayersByName(match.HostTeam);
                match.GuestTeamPlayers = TeamPlayersByName(match.GuestTeam);

                foreach (var i in infoFromDb)
                {
                    if (i.MatchId == match.MatchId)
                    {
                        if (TeamPlayersByName(match.GuestTeam).Contains(i.PlayerName))
                        {
                            scorersOfGuestTeam.Add(i.PlayerName+" : "+i.Goals);
                        }
                        else
                        {
                            scorersOfHostTeam.Add(i.PlayerName+" : "+i.Goals);
                        }
                    }
                }
                match.GuestTeamScorers = scorersOfGuestTeam;
                match.HostTeamScorers = scorersOfHostTeam;
            }

            response.Data = result;
            response.Message = "Matches were successfully fetched.";
            return response;
        }
    }
}