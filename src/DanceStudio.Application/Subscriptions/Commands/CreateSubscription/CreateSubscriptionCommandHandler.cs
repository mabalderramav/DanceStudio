using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Subscriptions;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler :
        IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionsRepository subscriptionsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAdminsRepository adminsRepository;

        public CreateSubscriptionCommandHandler(
            ISubscriptionsRepository subscriptionsRepository,
            IUnitOfWork unitOfWork,
            IAdminsRepository adminsRepository)
        {
            this.subscriptionsRepository = subscriptionsRepository;
            this.unitOfWork = unitOfWork;
            this.adminsRepository = adminsRepository;
        }
        public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request,
            CancellationToken cancellationToken)
        {
            //validations and application bussiness rules
            var admin = await adminsRepository.GetByIdAsync(request.AdminId);

            if (admin is null)
                return Error.NotFound("The admin wasn't found");

            if (admin.SubscriptionId is not null)
                return Error.Conflict("The admin has already an active subscription");


            //Create a subscription
            var subscription = new Subscription(request.SubscriptionType, request.AdminId);
            admin.SetSubscription(subscription);

            //Add it to the persistence tech, or DB
            await subscriptionsRepository.AddSubscriptionAsync(subscription);
            await adminsRepository.UpdateAsync(admin);
            await unitOfWork.CommitChangesAsync();

            //return subscription
            return subscription;
        }
    }
}
