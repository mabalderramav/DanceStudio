using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Profiles.Queries.ListProfiles
{
    public record ListProfilesQuery(Guid UserId) : IRequest<ErrorOr<ListProfilesResult>>;
}