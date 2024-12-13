using DanceStudio.Domain.Studios;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Queries.GetStudio;

public record GetStudioQuery(Guid SubscriptionId, Guid StudioId) : IRequest<ErrorOr<Studio>>;