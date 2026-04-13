using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Admins.Events;
using MediatR;

namespace DanceStudio.Application.Studios.Events;

public class SubscriptionDeletedEventHandler(
    IStudiosRepository studiosRepository,
    IUnitOfWork unitOfWork)
    : INotificationHandler<SubscriptionDeletedEvent>
{
    public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
    {
        var studios = await studiosRepository.ListBySubscriptionIdAsync(notification.SubscriptionId);
        await studiosRepository.RemoveRangeAsync(studios);
        await unitOfWork.CommitChangesAsync();
    }
}