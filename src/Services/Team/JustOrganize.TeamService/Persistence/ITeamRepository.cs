using JustOrganize.TeamService.Models;
using System.Collections.Generic;

namespace JustOrganize.TeamService.Persistence
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetTeams();
        void AddTeam(Team team);
    }
}
