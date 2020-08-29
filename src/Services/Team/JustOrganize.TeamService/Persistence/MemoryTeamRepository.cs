using JustOrganize.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JustOrganize.TeamService.Persistence
{
    public class MemoryTeamRepository : ITeamRepository
    {

        protected static ICollection<Team> teams;

        public MemoryTeamRepository()
        {
            if (teams == null)
            {
                teams = new List<Team>();
            }
        }

        public MemoryTeamRepository(ICollection<Team> teamsList)
        {
            teams = teamsList;
        }

        public void AddTeam(Team team)
        {
            teams.Add(team);
        }

        public Team GetTeam(Guid teamId)
        {
            return teams.FirstOrDefault(t => t.Id == teamId);
        }

        public IEnumerable<Team> GetTeams()
        {
            return teams;
        }
    }
}
