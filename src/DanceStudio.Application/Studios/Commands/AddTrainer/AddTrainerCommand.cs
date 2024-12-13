using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Commands.AddTrainer;

public record AddTrainerCommand(Guid SubscriptionId, Guid StudioId, Guid TrainerId)
    :IRequest<ErrorOr<Success>>;