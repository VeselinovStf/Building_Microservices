using JustOrganize.TeamService.Controllers;
using JustOrganize.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JustOrganize.TeamService.Tests
{
    public class TeamControllerTests
    {
        [Fact]
        public async void QueryTeamListReturnsCorrectTeams()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            var rawTeams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;

            var teams = new List<Team>(rawTeams);

            Assert.Equal(2, teams.Count);
            Assert.Equal("one", teams[0].Name);
            Assert.Equal("two", teams[1].Name);
        }

        [Fact]
        public async void CreateTeamAddsTeamToList()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            var teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;

            List<Team> original = new List<Team>(teams);

            Team t = new Team("sample");

            var result = await controller.CreateTeam(t);

            Assert.Equal(201, (result as ObjectResult).StatusCode);

            var newTeamsRaw =
                (IEnumerable<Team>)
                (await controller.GetAllTeams() as ObjectResult).Value;

            List<Team> newTeams = new List<Team>(newTeamsRaw);

            Assert.Equal(original.Count + 1, newTeams.Count);

            var sampleTeam =
                newTeams.FirstOrDefault(target => target.Name == "sample");

            Assert.NotNull(sampleTeam);
        }
    }
}
