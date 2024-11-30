using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Subscriptions;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionsRepository subscriptionsRepository;

        public GetSubscriptionQueryHandler(ISubscriptionsRepository subscriptionsRepository)
        {
            this.subscriptionsRepository = subscriptionsRepository;
        }
        public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery query, CancellationToken cancellationToken)
        {
            var subscription = await subscriptionsRepository.GetByIdAsync(query.SubscriptionId);
            return subscription is null
                ? Error.NotFound("Subscription does not exist.")
                : subscription;
        }
    }
}
