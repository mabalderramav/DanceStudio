using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Subscriptions;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQueryHandler(ISubscriptionsRepository subscriptionsRepository)
        : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
    {
        public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery query, CancellationToken cancellationToken)
        {
            var subscription = await subscriptionsRepository.GetByIdAsync(query.SubscriptionId);
            return subscription is null
                ? Error.NotFound("Subscription does not exist.")
                : subscription;
        }
    }
}
