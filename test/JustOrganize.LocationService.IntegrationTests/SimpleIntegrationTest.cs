using JustOrganize.LocationService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JustOrganize.LocationService.IntegrationTests
{
    public class SimpleIntegrationTest
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _testClient;

        private readonly LocationRecord _testLocation;

        public SimpleIntegrationTest()
        {
            _testServer = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            _testClient = _testServer.CreateClient();

            _testLocation = new LocationRecord()
            {
                Id = Guid.NewGuid(),
                Lattitude = 123456.2f
            };
        }

        [Fact]
        public async Task TestLocationPostAndGet()
        {
            StringContent stringContent = new StringContent(
                    JsonConvert.SerializeObject(_testLocation),
                    UnicodeEncoding.UTF8,
                    "application/json");

            HttpResponseMessage postResponse = await _testClient.PostAsync(
                "api/locations",
                stringContent
                );

            postResponse.EnsureSuccessStatusCode();

            var getResponse = await _testClient.GetAsync(
                    "api/locations"
                );

            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();

            List<LocationRecord> locations = JsonConvert.DeserializeObject<List<LocationRecord>>(raw);

            Assert.Single(locations);
            Assert.Equal(_testLocation.Lattitude, locations[0].Lattitude);
            Assert.Equal(_testLocation.Id, locations[0].Id);


        }
    }
}
