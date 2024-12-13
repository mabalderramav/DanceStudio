using DanceStudio.Domain.Studios;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Commands.CreateStudio;

public record CreateStudioCommand(string Name, Guid SubscriptionId) : 
    IRequest<ErrorOr<Studio>>;