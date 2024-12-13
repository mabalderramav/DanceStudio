using DanceStudio.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
    {
        private readonly IAdminsRepository adminsRepository;
        private readonly ISubscriptionsRepository subscriptionsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IStudiosRepository studiosRepository;

        public DeleteSubscriptionCommandHandler(
            IAdminsRepository adminsRepository,
            ISubscriptionsRepository subscriptionsRepository,
            IUnitOfWork unitOfWork,
            IStudiosRepository studiosRepository)
        {
            this.adminsRepository = adminsRepository;
            this.subscriptionsRepository = subscriptionsRepository;
            this.unitOfWork = unitOfWork;
            this.studiosRepository = studiosRepository;
        }
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
