using JustOrganize.LocationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustOrganize.LocationService.Persistance
{
    public interface ILocationRepository
    {
        Task<LocationRecord> GetMemberLocationHistoryAsync(string memberId);
        Task<ICollection<LocationRecord>> GetAllLocationRecordsAsync();
        Task AddRecordAsync(LocationRecord newRecord);
    }
}
