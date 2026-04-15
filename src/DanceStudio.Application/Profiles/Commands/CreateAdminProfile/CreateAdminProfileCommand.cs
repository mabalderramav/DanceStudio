using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Profiles.Command.CreateAdminProfile
{
    public record CreateAdminProfileCommand(Guid UserId) : IRequest<ErrorOr<Guid>>;
}