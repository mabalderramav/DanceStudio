using DanceStudio.Application.Rooms.Commands.CreateRoom;
using DanceStudio.Application.Rooms.Commands.DeleteRoom;
using DanceStudio.Contracts.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Api.Controllers
{
    [Route("api/{studioId:guid}/rooms")]
    public class RoomsController : ApiController
    {
        private readonly ISender sender;

        public RoomsController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomRequest request, Guid studioId)
        {
            var command = new CreateRoomCommand(studioId, request.Name);
            var createRoomResult = await sender.Send(command);
            return createRoomResult.Match(
                room => Created(
                     $"rooms/{room.Id}",
                     new RoomResponse(room.Id, room.Name)
                    ),
                Problem);
        }

        [HttpDelete("{roomId:guid}")]
        public async Task<IActionResult> DeleteRoom(Guid roomId, Guid studioId)
        {
            var command = new DeleteRoomCommand(studioId, roomId);
            var deleteRoomResult = await sender.Send(command);
            return deleteRoomResult.Match(
                _ => NoContent(),
                Problem);
        }
    }
}
