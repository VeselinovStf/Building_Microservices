using JustOrganize.LocationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
