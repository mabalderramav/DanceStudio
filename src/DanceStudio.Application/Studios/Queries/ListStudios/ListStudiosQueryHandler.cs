using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Studios;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Queries.ListStudios
{
    public class ListStudiosQueryHandler(
        IStudiosRepository studiosRepository,
        ISubscriptionsRepository subscriptionsRepository) : IRequestHandler<ListStudiosQuery, ErrorOr<List<Studio>>>
    {
        private readonly IStudiosRepository studiosRepository = studiosRepository;
        private readonly ISubscriptionsRepository subscriptionsRepository = subscriptionsRepository;

        public async Task<ErrorOr<List<Studio>>> Handle(ListStudiosQuery request, CancellationToken cancellationToken)
        {
            if (!await subscriptionsRepository.ExistsAsync(request.SubscriptionId))
                return Error.NotFound(description: "Subscription not found");

            return await studiosRepository.ListBySubscriptionIdAsync(request.SubscriptionId);
        }
    }
}
