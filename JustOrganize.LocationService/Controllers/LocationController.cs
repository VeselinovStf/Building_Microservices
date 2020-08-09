using JustOrganize.LocationService.Models;
using JustOrganize.LocationService.Persistance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustOrganize.LocationService.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(
            ILocationRepository locationRepository
            )
        {
            this._locationRepository = locationRepository;
        }

        [HttpGet]
        [Route("{memberId}")]
        public async Task<IActionResult> RetrieveLocationHistrory(string memberId)
        {
            return Ok(await this._locationRepository.GetMemberLocationHistoryAsync(memberId));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocationRecords()
        {
            return Ok(await this._locationRepository.GetAllLocationRecordsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddMemberLocationRecord([FromBody]LocationRecord newRecord)
        {
            this._locationRepository.AddRecordAsync(newRecord);

            return Created("", newRecord);
        }
    }
}
