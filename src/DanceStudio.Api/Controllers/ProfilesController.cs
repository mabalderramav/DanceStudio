using DanceStudio.Application.Profiles.Command.CreateAdminProfile;
using DanceStudio.Application.Profiles.Queries.ListProfiles;
using DanceStudio.Contracts.Profiles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Api.Controllers
{
    [Route("users/{userId:guid}/profiles")]
    [Authorize]
    public class ProfilesController(ISender sender) : ApiController
    {
        [HttpPost("admin")]
        public async Task<IActionResult> CreateAdminProfile(Guid userId)
        {
            var command = new CreateAdminProfileCommand(userId);

            var createProfileResult = await sender.Send(command);

            return createProfileResult.Match(
                id => Ok(new ProfileResponse(id)),
                Problem);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ListProfiles(Guid userId)
        {
            var listProfilesQuery = new ListProfilesQuery(userId);

            var listProfilesResult = await sender.Send(listProfilesQuery);

            return listProfilesResult.Match(
                profiles => Ok(new ListProfilesResponse(
                    profiles.AdminId,
                    profiles.ParticipantId,
                    profiles.TrainerId)),
                Problem);
        }
    }
}