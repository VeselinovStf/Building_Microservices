using JustOrganize.TeamService.Models;
using JustOrganize.TeamService.Persistence;
using System.Collections.Generic;

namespace JustOrganize.TeamService.Tests
{
    public class TestMemoryTeamRepository : MemoryTeamRepository
    {
        public TestMemoryTeamRepository() : base(CreateInitialFake())
        {

        }

        private static ICollection<Team> CreateInitialFake()
        {
            var teams = new List<Team>();

            teams.Add(new Team("one"));
            teams.Add(new Team("two"));

            return teams;
        }
    }
}
