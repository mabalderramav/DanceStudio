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

        public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository,
            IUnitOfWork unitOfWork)
        {
            this.subscriptionsRepository = subscriptionsRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request,
            CancellationToken cancellationToken)
        {
            //Create a subscription
            var subscription = new Subscription
            {
                Id = Guid.NewGuid()
            };

            //Add it to the persistence tech, or DB
            await subscriptionsRepository.AddSubscriptionAsync(subscription);
            await unitOfWork.CommitChangesAsync();

            //return subscription
            return subscription;
        }
    }
}
