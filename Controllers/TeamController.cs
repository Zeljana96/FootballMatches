using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task5.Data;
using Task5.Dtos.Player;
using Task5.Dtos.Team;
using Task5.Models;
using Task5.Services.MatchService;
using Task5.Services.PlayerService;
using Task5.Services.TeamService;

namespace Task5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IMatchService _matchService;
        private readonly IPlayerService _playerService;
        public TeamController(IMapper mapper, ITeamService teamService, DataContext context, IMatchService matchService, IPlayerService playerService)
        {
            _playerService = playerService;
            _matchService = matchService;
            _context = context;
            _mapper = mapper;
            _teamService = teamService;


        }
        public async Task<IActionResult> Index()
        {
           IEnumerable<GetTeamDto> allTeams = await _teamService.GetAllTeams();
           List<int> numberOfPlayersByTeam = await _teamService.NumberOfPlayersByTeam();
           List<GetTeamDto> lista = allTeams.ToList();
           for(int i = 0; i < lista.Count(); i++)
           {
               lista[i].NumberOfPlayers = numberOfPlayersByTeam[i];
           }
            ViewBag.Teams = lista;
            return View();
        }
        // this approach did not give required functionality
        [HttpGet("create")]
        public IActionResult Create()
        {
            AddTeamDto teamDto = new AddTeamDto();
            return PartialView("_TeamModalPartial", teamDto);
        }

        // alternative ActionResult for creating team
        [HttpGet("create2")]
        public IActionResult AlternativeCreate()
        {
            AddTeamDto newTeam = new AddTeamDto();
            return View(newTeam);
        }

        [HttpPost("addTeam")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeam([FromForm]AddTeamDto newTeam)
        {
            ServiceResponse<Team> addedTeam = await _teamService.addNewTeam(newTeam);
            if(addedTeam != null)
            {
                return RedirectToAction("Index");
            }
            
            return BadRequest("Something went wrong with saving team to Db.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            GetTeamDto requiredTeam = await _teamService.GetTeamById(id);
            if (requiredTeam == null)
            {
                return NotFound();
            }
            return View(requiredTeam);
        }
        
        [HttpGet("DeletePlayer/{playerName}")]
        public async Task<ActionResult> DeletePlayer(string playerName)
        {
            bool deleted = await _playerService.DeletePlayer(playerName);
            return RedirectToAction("Details", new { id = playerName.Split("+")[1]});
        }
        [HttpGet("addPlayer/{teamId}")]
        public ActionResult AddPlayer(int teamId)
        {
            AddPlayerDto player = new AddPlayerDto();
            player.TeamId = teamId;
            ViewBag.Team = teamId;
            return View(player);
        }
        [HttpPost]
        public async Task<ActionResult> CreatePlayer([FromForm]AddPlayerDto newPlayer)
        { 
           await _playerService.AddPlayerToDb(newPlayer);
            return RedirectToAction("Details", new { id = newPlayer.TeamId});
        }
    }
}