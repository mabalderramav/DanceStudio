using DanceStudio.Application.Subscriptions.Commands.CreateSubscription;
using DanceStudio.Contracts.Subcriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public SubscriptionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
        {
            var command = new CreateSubscriptionCommand(
                request.SubscriptionType.ToString(),
                request.AdminId);

            var createSubscriptionResult = await mediator.Send(command);

            return createSubscriptionResult.MatchFirst(
                subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
                error => Problem()
                );
        }
    }
}
