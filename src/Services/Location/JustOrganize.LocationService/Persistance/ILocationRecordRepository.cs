using JustOrganize.LocationService.Models;
using System;
using System.Collections.Generic;

namespace JustOrganize.LocationService.Persistance
{
    public interface ILocationRecordRepository
    {
        LocationRecord Add(LocationRecord newRecord);

        LocationRecord Update(LocationRecord locationRecord);

        LocationRecord Get(Guid memberId, Guid recordId);

        LocationRecord Delete(Guid memberId, Guid recordId);

        LocationRecord GetLatestForMember(Guid memberId);

        ICollection<LocationRecord> AllForMember(Guid memberId);


    }
}
