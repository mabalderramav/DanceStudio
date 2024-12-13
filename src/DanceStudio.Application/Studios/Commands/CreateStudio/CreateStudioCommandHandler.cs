using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Studios;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Commands.CreateStudio
{
    public class CreateStudioCommandHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IStudiosRepository studiosRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateStudioCommand, ErrorOr<Studio>>
    {
        private readonly ISubscriptionsRepository subscriptionsRepository = subscriptionsRepository;
        private readonly IStudiosRepository studiosRepository = studiosRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<Studio>> Handle(CreateStudioCommand request, CancellationToken cancellationToken)
        {
            var subscription = await subscriptionsRepository.GetByIdAsync(request.SubscriptionId);

            if (subscription is null)
                return Error.NotFound(description: "Subscription not found");

            var studio = new Studio(
                name: request.Name,
                maxRooms: subscription.GetMaxRooms(),
                subscriptionId: subscription.Id);

            var addStudioResult = subscription.AddStudio(studio);

            if (addStudioResult.IsError)
                return addStudioResult.Errors;

            await subscriptionsRepository.UpdateAsync(subscription);
            await studiosRepository.AddAsync(studio);
            await unitOfWork.CommitChangesAsync();
            return studio;
        }
    }
}
