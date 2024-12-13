using DanceStudio.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Commands.DeleteStudio
{
    public class DeleteStudioCommandHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IStudiosRepository studiosRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteStudioCommand, ErrorOr<Deleted>>
    {
        private readonly ISubscriptionsRepository subscriptionsRepository = subscriptionsRepository;
        private readonly IStudiosRepository studiosRepository = studiosRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<Deleted>> Handle(DeleteStudioCommand request, CancellationToken cancellationToken)
        {
            var studio = await studiosRepository.GetByIdAsync(request.StudioId);

            if (studio is null)
                return Error.NotFound(description: "Studio not found");

            var subscription = await subscriptionsRepository.GetByIdAsync(request.SubscriptionId);

            if (subscription is null)
                return Error.NotFound(description: "Subscription not found");

            if (!subscription.HasStudio(request.StudioId))
                return Error.Unexpected(description: "Studio not found");

            subscription.RemoveStudio(request.StudioId);

            await subscriptionsRepository.UpdateAsync(subscription);
            await studiosRepository.RemoveAsync(studio);
            await unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }
    }
}
