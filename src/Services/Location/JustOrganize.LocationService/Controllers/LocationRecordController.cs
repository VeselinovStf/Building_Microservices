using JustOrganize.LocationService.Models;
using JustOrganize.LocationService.Persistance;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JustOrganize.LocationService.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationRecordController : ControllerBase
    {
        private readonly ILocationRecordRepository _locationRepository;

        public LocationRecordController(
            ILocationRecordRepository locationRepository
            )
        {
            this._locationRepository = locationRepository;
        }

        [HttpGet]
        public IActionResult GetLocationsForMember(Guid memberId)
        {
            return Ok(this._locationRepository.AllForMember(memberId));
        }

        [HttpGet("latest")]
        public IActionResult GetLatestForMember(Guid memberId)
        {
            return Ok(this._locationRepository.GetLatestForMember(memberId));
        }

        [HttpPost]
        public IActionResult AddLocation(Guid memberId,
            [FromBody] LocationRecord newRecord)
        {
            this._locationRepository.Add(newRecord);

            return Created($"/api/locations/{memberId}/{newRecord.Id}", newRecord);
        }
    }
}
