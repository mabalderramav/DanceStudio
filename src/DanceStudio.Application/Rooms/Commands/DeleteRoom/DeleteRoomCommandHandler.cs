using DanceStudio.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ErrorOr<Deleted>>
    {
        private readonly IStudiosRepository studiosRepository;
        private readonly ISubscriptionsRepository subscriptionsRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteRoomCommandHandler(
            IStudiosRepository studiosRepository,
            ISubscriptionsRepository subscriptionsRepository,
            IUnitOfWork unitOfWork
        )
        {
            this.studiosRepository = studiosRepository;
            this.subscriptionsRepository = subscriptionsRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var studio = await studiosRepository.GetByIdAsync(request.StudioId);

            if (studio is null)
                return Error.NotFound(description: "Studio not found");

            if (!studio.HasRoom(request.RoomId))
                return Error.NotFound(description: "Room not found");

            studio.RemoveRoom(request.RoomId);

            await studiosRepository.UpdateAsync(studio);
            await unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}
