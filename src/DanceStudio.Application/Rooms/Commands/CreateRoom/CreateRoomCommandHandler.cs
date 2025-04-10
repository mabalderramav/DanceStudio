using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Rooms;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IStudiosRepository studiosRepository,
        IUnitOfWork unitOfWork
            ) : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
    {
        public IUnitOfWork UnitOfWork { get; } = unitOfWork;
        public IStudiosRepository StudiosRepository { get; } = studiosRepository;

        public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var studio = await StudiosRepository.GetByIdAsync(request.StudioId);

            if (studio is null)
                return Error.NotFound(description: "Studio not found");

            var subscription = await subscriptionsRepository.GetByIdAsync(studio.SubscriptionId);

            if (subscription is null)
                return Error.Unexpected(description: "Subscription not found");

            var room = new Room(request.RoomName, studio.Id);

            var addStudioResult = studio.AddRoom(room);

            if (addStudioResult.IsError)
                return addStudioResult.Errors;

            await studiosRepository.UpdateAsync(studio);
            await unitOfWork.CommitChangesAsync();

            return room;
        }
    }
}
