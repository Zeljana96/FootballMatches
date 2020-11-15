using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task5.Data;
using Task5.Dtos.Match;
using Task5.Services.MatchService;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Task5.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Task5.Dtos.Team;
using Task5.Services.TeamService;
using Task5.Dtos.Player;
using Task5.Services.PlayerService;
using AutoMapper;

namespace Task5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly DataContext _context;
        private readonly ITeamService _teamService;
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;
        public MatchController(IMatchService matchService, DataContext context, ITeamService teamService, IPlayerService playerService, IMapper mapper)
        {
            _mapper = mapper;
            _playerService = playerService;
            _teamService = teamService;
            _context = context;
            _matchService = matchService;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<GetMatchDto> allMatches = await _matchService.GetAllMatches();
            ViewBag.Matches = allMatches;
            return View();
        }
        [HttpGet("createMatch")]
        public IActionResult CreateView()
        {
            ViewBag.TeamName = new SelectList(_context.Teams, "TeamId", "Name");
            AddMatchDto newMatch = new AddMatchDto();
            ViewBag.Match = newMatch;
            return View();
        }
        // this ActionResult can be tested only in Postman
        [HttpPost]
        public async Task<IActionResult> Create(AddMatchDto newMatch)
        {
            ServiceResponse<List<GetMatchDto>> response = await _matchService.AddMatch(newMatch);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        // for testing purposes
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeamPlayers(int id)
        {
            List<Player> requiredPlayers = await _matchService.TeamPlayersById(id);
            ViewBag.PlayerList = new SelectList(requiredPlayers, "PlayerId", "Name");
            return Ok(requiredPlayers);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult> Statistics()
        {
            List<string> teamStatistics = await _matchService.TeamStatistic();
            IEnumerable<Player> playerStatistics = await _matchService.PlayerStatistic();
            ViewBag.TeamStatistics = teamStatistics;
            ViewBag.PlayerStatistics = playerStatistics;
            return View();
        }
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            GetMatchDto requiredMatch = await _matchService.GetMatchById(id);

            int hostTeamId = await _teamService.GetTeamIdByName(requiredMatch.HostTeam);
            GetTeamDto hostTeam = await _teamService.GetTeamById(hostTeamId);

            int guestTeamId = await _teamService.GetTeamIdByName(requiredMatch.GuestTeam);
            GetTeamDto guestTeam = await _teamService.GetTeamById(guestTeamId);
            if (requiredMatch == null)
            {
                return NotFound();
            }
            ViewBag.Guest = guestTeam.Players;
            ViewBag.Host = hostTeam.Players;
            return View(requiredMatch);
        }
        [HttpGet("addGoal/{data}")]
        public async Task<IActionResult> AddGoal(string data)
        {
            await _matchService.Goal(data.Split("+")[1], data.Split("+")[0]);
            return RedirectToAction("Details", new { id = data.Split("+")[1] });
        }
        //this method is tested only in Postman
        [HttpPost("goal")]
        public async Task<IActionResult> PlayerScored(PlayerGoalDto playerGoal)
        {
            // part for Rest task
            
            PlayerGoalMatch playerFromDb = await _context.PlayerGoalMatches.FirstOrDefaultAsync(
                p => p.PlayerName.ToLower().Equals(playerGoal.PlayerName.ToLower()) && p.MatchId == int.Parse(playerGoal.MatchId));
            if(playerFromDb != null)
            {
                playerFromDb.Goals = playerFromDb.Goals + 1;

                _context.PlayerGoalMatches.Update(playerFromDb);
                await _context.SaveChangesAsync();
            }else
            {
                PlayerGoalMatch newForDb = new PlayerGoalMatch();
                newForDb.PlayerName = playerGoal.PlayerName;
                newForDb.MatchId = int.Parse(playerGoal.MatchId);
                newForDb.Goals = 1;
                await _context.PlayerGoalMatches.AddAsync(newForDb);
                await _context.SaveChangesAsync();
            } 
            //    

            await _matchService.Goal(playerGoal.MatchId, playerGoal.TeamName);
            await _playerService.PlayerScored(playerGoal.PlayerName, playerGoal.MatchId);
            Match matchFromDb = await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == int.Parse(playerGoal.MatchId));
            GetMatchDto match = _mapper.Map<GetMatchDto>(matchFromDb);
            return Ok(match);
        }

    // Second part: Rest API
    [HttpGet("rest")]
     public async Task<IActionResult> AllMatches()
        {

            return Ok(await _matchService.GetAllMatchesRest());
        }

    }
}