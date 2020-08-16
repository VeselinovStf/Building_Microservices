using JustOrganize.LocationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JustOrganize.LocationService.Persistance
{
    public class InMemoryLocationRecordRepository : ILocationRecordRepository
    {
        protected static ICollection<LocationRecord> locationRecords;

        public InMemoryLocationRecordRepository()
        {
            if (locationRecords == null)
            {
                locationRecords = new List<LocationRecord>();
            }
        }

        public InMemoryLocationRecordRepository(ICollection<LocationRecord> loctionRecordsList)
        {
            locationRecords = loctionRecordsList;
        }

        public LocationRecord Add(LocationRecord locationRecord)
        {
            locationRecords.Add(locationRecord);

            return locationRecord;
        }

        public LocationRecord Update(LocationRecord locationRecord)
        {
            LocationRecord currentLocationRecord = this.Get(locationRecord.MemberId, locationRecord.Id);

            currentLocationRecord = locationRecord;

            return locationRecord;
        }

        public LocationRecord Get(Guid memberId, Guid recordId)
        {
            return locationRecords.FirstOrDefault(lr => lr.MemberId == memberId && lr.Id == recordId);
        }

        public LocationRecord Delete(Guid memberId, Guid recordId)
        {
            LocationRecord locationRecord = this.Get(memberId, recordId);
            locationRecords.Remove(locationRecord);

            return locationRecord;
        }

        public LocationRecord GetLatestForMember(Guid memberId)
        {
            

            LocationRecord locationRecord = locationRecords.
                Where(lr => lr.MemberId == memberId).
                OrderBy(lr => lr.Timestamp).
                LastOrDefault();

            return locationRecord;
        }

        public ICollection<LocationRecord> AllForMember(Guid memberId)
        {
            return locationRecords.
                Where(lr => lr.MemberId == memberId).
                OrderBy(lr => lr.Timestamp).
                ToList();
        }
    }
}
