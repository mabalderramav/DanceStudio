using DanceStudio.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler(
        IAdminsRepository adminsRepository,
        ISubscriptionsRepository subscriptionsRepository,
        IUnitOfWork unitOfWork,
        IStudiosRepository studiosRepository)
        : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
    {
        public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await subscriptionsRepository.GetByIdAsync(request.SubscriptionId);

            if (subscription is null)
                return Error.NotFound("Subscription not found");

            var admin = await adminsRepository.GetByIdAsync(subscription.AdminId);

            if (admin is null) return Error.Unexpected("Admin not found");

            admin.DeleteSubscription(request.SubscriptionId);

            var studiosToDelete = await studiosRepository.ListBySubscriptionIdAsync(request.SubscriptionId);

            await adminsRepository.UpdateAsync(admin);
            await subscriptionsRepository.RemoveSubscriptionAsync(subscription);
            await studiosRepository.RemoveRangeAsync(studiosToDelete);
            await unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }
    }
}
