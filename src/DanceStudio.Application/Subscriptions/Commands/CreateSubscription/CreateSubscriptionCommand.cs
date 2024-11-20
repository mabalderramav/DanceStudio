using DanceStudio.Domain.Subscriptions;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Subscriptions.Commands.CreateSubscription
{
    public record CreateSubscriptionCommand(
        string SubscriptionType,
        Guid AdminId) : IRequest<ErrorOr<Subscription>>;

}
