using JustOrganize.LocationService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustOrganize.LocationService.Persistance
{
    public class MemoryLocationRepository : ILocationRepository
    {
        protected static ICollection<LocationRecord> locationRecords;

        public MemoryLocationRepository()
        {
            if (locationRecords == null)
            {
                locationRecords = new List<LocationRecord>();
            }
        }

        public MemoryLocationRepository(ICollection<LocationRecord> loctionRecordsList)
        {
            locationRecords = loctionRecordsList;
        }

        public async Task AddRecordAsync(LocationRecord newRecord)
        {
            locationRecords.Add(newRecord);
        }

        public async Task<ICollection<LocationRecord>> GetAllLocationRecordsAsync()
        {
            return locationRecords;
        }

        public async Task<LocationRecord> GetMemberLocationHistoryAsync(string memberId)
        {
            return locationRecords.FirstOrDefault(r => r.MemberId.ToString() == memberId);
        }
    }
}
