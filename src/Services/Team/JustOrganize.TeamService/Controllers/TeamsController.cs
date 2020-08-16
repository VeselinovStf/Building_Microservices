using JustOrganize.TeamService.Models;
using JustOrganize.TeamService.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JustOrganize.TeamService.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            this._teamRepository = teamRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            return Ok(this._teamRepository.GetTeams());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] Team newTeam)
        {
            this._teamRepository.AddTeam(newTeam);

            return Created("", newTeam);
        }
    }
}
