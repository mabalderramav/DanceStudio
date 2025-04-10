using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Rooms.Commands.DeleteRoom
{
    public record DeleteRoomCommand(Guid StudioId, Guid RoomId) : IRequest<ErrorOr<Deleted>>;
}


