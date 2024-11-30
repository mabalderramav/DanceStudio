using DanceStudio.Application.Subscriptions.Commands.CreateSubscription;
using DanceStudio.Application.Subscriptions.Queries.GetSubscription;
using DanceStudio.Contracts.Subcriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = DanceStudio.Domain.Subscriptions.SubscriptionType;

namespace DanceStudio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISender mediator;

        public SubscriptionsController(ISender mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
        {
            if (!DomainSubscriptionType.TryFromName(request.SubscriptionType.ToString(),
                out var subscriptionType))
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest,
                    detail: "Invalid supscription type.");
            }

            var command = new CreateSubscriptionCommand(subscriptionType, request.AdminId);

            var createSubscriptionResult = await mediator.Send(command);

            return createSubscriptionResult.MatchFirst(
                subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
                error => Problem()
                );
        }

        [HttpGet("{subscriptionId:guid}")]
        public async Task<IActionResult> GetSubscription(Guid subscriptionId)
        {
            var query = new GetSubscriptionQuery(subscriptionId);
            var getSubscriptionResult = await mediator.Send(query);
            return getSubscriptionResult.MatchFirst(
                subscription => Ok(new SubscriptionResponse(subscription.Id,
                    Enum.Parse<SubscriptionType>(subscription.SubscriptionType.Name))),
                error => Problem()
                );
        }
    }
}
