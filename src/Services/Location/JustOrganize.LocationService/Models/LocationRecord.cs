using System;

namespace JustOrganize.LocationService.Models
{
    public class LocationRecord
    {
        public Guid Id { get; set; }

        public float Lattitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }

        public long Timestamp { get; set; }

        public Guid MemberId { get; set; }

    }
}
