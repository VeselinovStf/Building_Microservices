using JustOrganize.LocationService.Controllers;
using JustOrganize.LocationService.Models;
using JustOrganize.LocationService.Persistance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JustOrganize.LocationService.Tests
{
    public class LocationControllerTests
    {
        [Fact]
        public void ShouldAdd()
        {
            ILocationRecordRepository repository = new InMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);
            Guid memberGuid = Guid.NewGuid();

            controller.AddLocation(memberGuid, new LocationRecord()
            {
                Id = Guid.NewGuid(),
                MemberId = memberGuid,
                Timestamp = 1
            });
            controller.AddLocation(memberGuid, new LocationRecord()
            {
                Id = Guid.NewGuid(),
                MemberId = memberGuid,
                Timestamp = 2
            });

            Assert.Equal(2, repository.AllForMember(memberGuid).Count());
        }

        [Fact]
        public void ShouldReturnEmtpyListForNewMember()
        {
            ILocationRecordRepository repository = new InMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);
            Guid memberGuid = Guid.NewGuid();

            ICollection<LocationRecord> locationRecords =
                ((controller.GetLocationsForMember(memberGuid) as ObjectResult).Value as ICollection<LocationRecord>);

            Assert.Equal(0, locationRecords.Count());
        }

        [Fact]
        public void ShouldTrackAllLocationsForMember()
        {
            ILocationRecordRepository repository = new InMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);
            Guid memberGuid = Guid.NewGuid();

            controller.AddLocation(memberGuid, new LocationRecord()
            {
                Id = Guid.NewGuid(),
                Timestamp = 1,
                MemberId = memberGuid,
                Lattitude = 12.3f
            });
            controller.AddLocation(memberGuid, new LocationRecord()
            {
                Id = Guid.NewGuid(),
                Timestamp = 2,
                MemberId = memberGuid,
                Lattitude = 23.4f
            });
            controller.AddLocation(Guid.NewGuid(), new LocationRecord()
            {
                Id = Guid.NewGuid(),
                Timestamp = 3,
                MemberId = Guid.NewGuid(),
                Lattitude = 23.4f
            });

            ICollection<LocationRecord> locationRecords =
                ((controller.GetLocationsForMember(memberGuid) as ObjectResult).Value as ICollection<LocationRecord>);

            Assert.Equal(2, locationRecords.Count());
        }

        [Fact]
        public void ShouldTrackNullLatestForNewMember()
        {
            ILocationRecordRepository repository = new InMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);

            Guid memberGuid = Guid.NewGuid();

            LocationRecord latest = ((controller.GetLatestForMember(memberGuid) as ObjectResult).Value as LocationRecord);

            Assert.Null(latest);
        }

        [Fact]
        public void ShouldTrackLatestLocationsForMember()
        {
            ILocationRecordRepository repository = new InMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);
            Guid memberGuid = Guid.NewGuid();

            Guid latestId = Guid.NewGuid();
            controller.AddLocation(memberGuid, new LocationRecord()
            {
                Id = Guid.NewGuid(),
                Timestamp = 1,
                MemberId = memberGuid,
                Lattitude = 12.3f
            });
            controller.AddLocation(memberGuid, new LocationRecord()
            {
                Id = latestId,
                Timestamp = 3,
                MemberId = memberGuid,
                Lattitude = 23.4f
            });
            controller.AddLocation(memberGuid, new LocationRecord()
            {
                Id = Guid.NewGuid(),
                Timestamp = 2,
                MemberId = memberGuid,
                Lattitude = 23.4f
            });
            controller.AddLocation(Guid.NewGuid(), new LocationRecord()
            {
                Id = Guid.NewGuid(),
                Timestamp = 4,
                MemberId = Guid.NewGuid(),
                Lattitude = 23.4f
            });

            LocationRecord latest = ((controller.GetLatestForMember(memberGuid) as ObjectResult).Value as LocationRecord);

            Assert.NotNull(latest);
            Assert.Equal(latestId, latest.Id);
        }
    }
}
