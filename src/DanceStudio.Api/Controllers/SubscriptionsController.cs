using DanceStudio.Application.Subscriptions.Commands.CreateSubscription;
using DanceStudio.Application.Subscriptions.Commands.DeleteSubscription;
using DanceStudio.Application.Subscriptions.Queries.GetSubscription;
using DanceStudio.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = DanceStudio.Domain.Subscriptions.SubscriptionType;

namespace DanceStudio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionsController(ISender mediator) : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
        {
            if (!DomainSubscriptionType.TryFromName(request.SubscriptionType.ToString(),
                out var subscriptionType))
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest,
                    detail: "Invalid subscription type");
            }

            var command = new CreateSubscriptionCommand(
                subscriptionType,
                request.AdminId);

            var createSubscriptionResult = await mediator.Send(command);

            return createSubscriptionResult.Match(
                subscription => CreatedAtAction(
                    nameof(GetSubscription),
                    new { subscriptionId = subscription.Id },
                    new SubscriptionResponse(subscription.Id, ToContract(subscription.SubscriptionType))),
                Problem
                    );
        }

        [HttpGet("{subscriptionId:guid}")]
        public async Task<IActionResult> GetSubscription(Guid subscriptionId)
        {
            var query = new GetSubscriptionQuery(subscriptionId);
            var getSubscriptionsResult = await mediator.Send(query);
            return getSubscriptionsResult.Match(
                subscription => Ok(new SubscriptionResponse(
                    subscription.Id,
                    ToContract(subscription.SubscriptionType))),
                Problem
                );
        }

        [HttpDelete("{subscriptionId:guid}")]
        public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
        {
            var command = new DeleteSubscriptionCommand(subscriptionId);
            var createSubscriptionResult = await mediator.Send(command);

            return createSubscriptionResult.Match(
                _ => NoContent(),
                Problem
                );
        }

        #region Utilities
        private static SubscriptionType ToContract(DomainSubscriptionType domainSubscriptionType)
        {
            return domainSubscriptionType.Name switch
            {
                nameof(DomainSubscriptionType.Free) => SubscriptionType.Free,
                nameof(DomainSubscriptionType.Starter) => SubscriptionType.Starter,
                nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
                _ => throw new InvalidOperationException()
            };
        }
        #endregion
    }
}
