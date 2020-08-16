using JustOrganize.TeamService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JustOrganize.TeamService.IntegrationTests
{
    public class SimpleIntegrationTest
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _testClient;

        private readonly Team _testTeam;

        public SimpleIntegrationTest()
        {
            _testServer = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            _testClient = _testServer.CreateClient();

            _testTeam = new Team()
            {
                Id = Guid.NewGuid(),
                Name = "Test Team 1"
            };
        }

        [Fact]
        public async Task TestTeamPostAndGet()
        {
            StringContent stringContent = new StringContent(
                    JsonConvert.SerializeObject(_testTeam),
                    UnicodeEncoding.UTF8,
                    "application/json");

            HttpResponseMessage postResponse = await _testClient.PostAsync(
                "api/teams",
                stringContent
                );

            postResponse.EnsureSuccessStatusCode();

            var getResponse = await _testClient.GetAsync(
                    "api/teams"
                );

            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();

            List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(raw);

            Assert.Single(teams);
            Assert.Equal(_testTeam.Name, teams[0].Name);
            Assert.Equal(_testTeam.Id, teams[0].Id);


        }
    }
}
