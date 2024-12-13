using DanceStudio.Domain.Studios;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Queries.ListStudios;

public record ListStudiosQuery(Guid SubscriptionId) : IRequest<ErrorOr<List<Studio>>>;