using JustOrganize.TeamService.Models.Location;
using System;
using System.Threading.Tasks;

namespace JustOrganize.TeamService.LocationClient.Abstraction
{
    public interface ILocationClient
    {
        Task<LocationRecord> GetLatestForMember(Guid memberId);
        Task<LocationRecord> AddLocation(Guid memberId, LocationRecord locationRecord);
    }
}
