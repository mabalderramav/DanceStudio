using DanceStudio.Application.Studios.Commands.AddTrainer;
using DanceStudio.Application.Studios.Commands.CreateStudio;
using DanceStudio.Application.Studios.Commands.DeleteStudio;
using DanceStudio.Application.Studios.Queries.GetStudio;
using DanceStudio.Application.Studios.Queries.ListStudios;
using DanceStudio.Contracts.Studios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Api.Controllers
{
    [Route("subscriptions/{subscriptionId:guid}/studios")]
    public class StudiosController : ApiController
    {
        private readonly ISender mediator;

        public StudiosController(ISender mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
        CreateStudioRequest request,
        Guid subscriptionId)
        {
            var command = new CreateStudioCommand(request.Name, subscriptionId);
            var createStudioResult = await mediator.Send(command);

            return createStudioResult.Match(
                studio => CreatedAtAction(
                    nameof(Get),
                    new { subscriptionId, StudioId = studio.Id },
                    new StudioResponse(studio.Id, studio.Name)),
                Problem);
        }

        [HttpDelete("{studioId:guid}")]
        public async Task<IActionResult> Delete(Guid subscriptionId, Guid studioId)
        {
            var command = new DeleteStudioCommand(subscriptionId, studioId);

            var deleteStudioResult = await mediator.Send(command);

            return deleteStudioResult.Match(
                _ => NoContent(),
                Problem);
        }

        [HttpGet]
        public async Task<IActionResult> List(Guid subscriptionId)
        {
            var command = new ListStudiosQuery(subscriptionId);
            var listStudiosResult = await mediator.Send(command);

            return listStudiosResult.Match(
                studios => Ok(studios.ConvertAll(studio => new StudioResponse(studio.Id, studio.Name))),
                Problem);
        }

        [HttpGet("{studioId:guid}")]
        public async Task<IActionResult> Get(Guid subscriptionId, Guid studioId)
        {
            var command = new GetStudioQuery(subscriptionId, studioId);
            var getStudioResult = await mediator.Send(command);

            return getStudioResult.Match(
                studio => Ok(new StudioResponse(studio.Id, studio.Name)),
                Problem);
        }

        [HttpPost("{studioId:guid}/trainers")]
        public async Task<IActionResult> AddTrainer(AddTrainerRequest request, Guid subscriptionId, Guid studioId)
        {
            var command = new AddTrainerCommand(subscriptionId, studioId, request.TrainerId);
            var addTrainerResult = await mediator.Send(command);

            return addTrainerResult.Match(
                success => Ok(),
                Problem);
        }
    }
}
