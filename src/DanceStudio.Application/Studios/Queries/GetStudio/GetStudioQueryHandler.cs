using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Studios;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Queries.GetStudio
{
    public class GetStudioQueryHandler(
        IStudiosRepository studiosRepository,
        ISubscriptionsRepository subscriptionsRepository) : IRequestHandler<GetStudioQuery, ErrorOr<Studio>>
    {
        private readonly IStudiosRepository studiosRepository = studiosRepository;
        private readonly ISubscriptionsRepository subscriptionsRepository = subscriptionsRepository;

        public async Task<ErrorOr<Studio>> Handle(GetStudioQuery request, CancellationToken cancellationToken)
        {
            if (!await subscriptionsRepository.ExistsAsync(request.SubscriptionId))
                return Error.NotFound("Subscription not found");
            
            if (await studiosRepository.GetByIdAsync(request.StudioId) is not Studio studio)
                return Error.NotFound(description: "Studio not found");

            return studio;
        }
    }
}
