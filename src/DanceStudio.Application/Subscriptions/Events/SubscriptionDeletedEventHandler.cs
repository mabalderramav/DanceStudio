using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Admins.Events;
using MediatR;

namespace DanceStudio.Application.Subscriptions.Events;

public class SubscriptionDeletedEventHandler(
    ISubscriptionsRepository subscriptionsRepository,
    IUnitOfWork unitOfWork)
    : INotificationHandler<SubscriptionDeletedEvent>
{
    public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionsRepository.GetByIdAsync(notification.SubscriptionId);

        if (subscription is null) throw new InvalidOperationException();

        await subscriptionsRepository.RemoveSubscriptionAsync(subscription);
        await unitOfWork.CommitChangesAsync();
    }
}