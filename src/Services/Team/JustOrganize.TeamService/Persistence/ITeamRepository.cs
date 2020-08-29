using JustOrganize.TeamService.Models;
using System;
using System.Collections.Generic;

namespace JustOrganize.TeamService.Persistence
{
    public interface ITeamRepository
    {
        Team GetTeam(Guid teamId);
        IEnumerable<Team> GetTeams();
        void AddTeam(Team team);
    }
}
