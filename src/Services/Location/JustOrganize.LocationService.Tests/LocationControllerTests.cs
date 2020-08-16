using JustOrganize.LocationService.Controllers;
using JustOrganize.LocationService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JustOrganize.LocationService.Tests
{
    public class LocationControllerTests
    {
        [Fact]
        public async Task RetrievesLocationHistoryOfAMember_When_Correct_MemberId()
        {
            var memberId = MemoryRepositoryConst.MEMBER_ID_1;

            var locationController = new LocationController(new LocationMemoryRepository());

            var memberLocationHistory = (LocationRecord)(
                await locationController.RetrieveLocationHistrory(memberId) as ObjectResult).Value;

            Assert.Equal(memberId, memberLocationHistory.MemberId.ToString());
            Assert.Equal(123.3f, memberLocationHistory.Lattitude);
            Assert.Equal(234.5f, memberLocationHistory.Longitude);
            Assert.Equal(345.6f, memberLocationHistory.Altitude);
            Assert.Equal((long)456.7, memberLocationHistory.Timestamp);
        }

        [Fact]
        public async Task Retrieves_Null_LocationHistoryOfAMember_When_Incorrect_MemberId()
        {
            var memberId = "123";

            var locationController = new LocationController(new LocationMemoryRepository());

            var memberLocationHistory = (LocationRecord)(
                await locationController.RetrieveLocationHistrory(memberId) as ObjectResult).Value;

            Assert.Null(memberLocationHistory);

        }

        [Fact]
        public async Task AddsLocationRecordToAMember()
        {
            var locationController = new LocationController(new LocationMemoryRepository());

            var newRecordMemberId = Guid.NewGuid();
            var newRecordId = Guid.NewGuid();
            float lattitude = 1231.3f;
            float longitude = 2341.5f;
            float altitude = 3451.6f;
            long timestamp = (long)4561.7;

            var newRecord = new LocationRecord()
            {
                Id = newRecordId,
                Lattitude = lattitude,
                Longitude = longitude,
                Altitude = altitude,
                Timestamp = timestamp,
                MemberId = newRecordMemberId,
            };

            var locationRecords = (IEnumerable<LocationRecord>)(await locationController.GetAllLocationRecords() as ObjectResult).Value;

            List<LocationRecord> original = new List<LocationRecord>(locationRecords);

            var result = await locationController.AddMemberLocationRecord(newRecord);

            Assert.Equal(201, (result as ObjectResult).StatusCode);

            var newestLocationRecords = (IEnumerable<LocationRecord>)(await locationController.GetAllLocationRecords() as ObjectResult).Value;

            List<LocationRecord> newLocations = new List<LocationRecord>(newestLocationRecords);

            Assert.Equal(original.Count + 1, newLocations.Count);

            var newAdded = newLocations
                .FirstOrDefault(l => l.Id == newRecordId);

            Assert.NotNull(newAdded);

            Assert.Equal(lattitude, newAdded.Lattitude);
            Assert.Equal(longitude, newAdded.Longitude);
            Assert.Equal(altitude, newAdded.Altitude);
            Assert.Equal(timestamp, newAdded.Timestamp);
            Assert.Equal(newRecordMemberId, newAdded.MemberId);

        }
    }
}
