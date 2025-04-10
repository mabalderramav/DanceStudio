using DanceStudio.Domain.Rooms;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(
        Guid StudioId,
        string RoomName
        ) : IRequest<ErrorOr<Room>>;
}
