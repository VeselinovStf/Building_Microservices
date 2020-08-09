using JustOrganize.LocationService.Models;
using JustOrganize.LocationService.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustOrganize.LocationService.Tests
{
    public static class MemoryRepositoryConst
    {
        public const string MEMBER_ID_1 = "1c1201f4-ff58-4b1e-b7ac-5238916a5a0d";
        public const string MEMBER_ID_2 = "2c1201f4-ff58-4b1e-b7ac-5238916a5a0d";
    }

    public class LocationMemoryRepository : MemoryLocationRepository
    {
        public LocationMemoryRepository() : base(CreateInitialFake())
        {

        }

        private static ICollection<LocationRecord> CreateInitialFake()
        {
            var locationRecords = new List<LocationRecord>();

            locationRecords.Add(
                new LocationRecord()
                {
                    Id = Guid.NewGuid(),
                    Lattitude = 123.3f,
                    Longitude = 234.5f,
                    Altitude = 345.6f,
                    Timestamp = (long)456.7,
                    MemberId = Guid.Parse(MemoryRepositoryConst.MEMBER_ID_1),
                });

            locationRecords.Add(
                new LocationRecord()
                {
                    Id = Guid.NewGuid(),
                    Lattitude = 12.3f,
                    Longitude = 23.5f,
                    Altitude = 34.6f,
                    Timestamp = (long)45.7,
                    MemberId = Guid.Parse(MemoryRepositoryConst.MEMBER_ID_2),
                });

            return locationRecords;
        }
    }
}
