using DanceStudio.Application.Common.Authorization;
using DanceStudio.Domain.Studios;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Commands.CreateStudio;

[Authorize(Roles = "Admin")]
public record CreateStudioCommand(string Name, Guid SubscriptionId) : 
    IRequest<ErrorOr<Studio>>;