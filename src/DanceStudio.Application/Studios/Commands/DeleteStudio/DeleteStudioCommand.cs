using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Commands.DeleteStudio;

public record DeleteStudioCommand(Guid SubscriptionId, Guid StudioId) : IRequest<ErrorOr<Deleted>>;