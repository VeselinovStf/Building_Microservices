using JustOrganize.TeamService.LocationClient.Abstraction;
using JustOrganize.TeamService.Models;
using JustOrganize.TeamService.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustOrganize.TeamService.Controllers
{
	[Route("/teams/{teamId}/[controller]")]
	public class MembersController : ControllerBase
	{
		private ITeamRepository repository;
		private ILocationClient locationClient;

		public MembersController(ITeamRepository repository, ILocationClient locationClient)
		{
			this.repository = repository;
			this.locationClient = locationClient;
		}

		[HttpGet]
		[Route("/teams/{teamId}/[controller]/{memberId}")]
		public async virtual Task<IActionResult> GetMember(Guid teamID, Guid memberId)
		{
			Team team = repository.GetTeam(teamID);

			if (team == null)
			{
				return this.NotFound();
			}
			else
			{
				var q = team.Members.Where(m => m.Id == memberId);

				if (q.Count() < 1)
				{
					return this.NotFound();
				}
				else
				{
					Member member = (Member)q.First();

					return this.Ok(new LocatedMember
					{
						Id = member.Id,
						FirstName = member.FirstName,
						LastName = member.LastName,
						LastLocation = await this.locationClient.GetLatestForMember(member.Id)
					});
				}
			}
		}

	}
}
