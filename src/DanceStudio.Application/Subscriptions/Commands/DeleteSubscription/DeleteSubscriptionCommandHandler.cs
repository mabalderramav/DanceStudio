using DanceStudio.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler(
        IAdminsRepository adminsRepository,
        ISubscriptionsRepository subscriptionsRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
    {
        public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand request,
            CancellationToken cancellationToken)
        {
            var subscription = await subscriptionsRepository.GetByIdAsync(request.SubscriptionId);

            var admin = await adminsRepository.GetByIdAsync(subscription.AdminId);

            if (admin is null) return Error.Unexpected("Admin not found");

            admin.DeleteSubscription(request.SubscriptionId);
            await adminsRepository.UpdateAsync(admin);
            await unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }
    }
}