using JustOrganize.TeamService.Models.Location;

namespace JustOrganize.TeamService.Models
{
    public class LocatedMember : Member
    {
        public LocationRecord LastLocation { get; set; }
    }
}
