using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Studios;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Commands.AddTrainer
{
    public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
    {
        private readonly IStudiosRepository studiosRepository;
        private readonly IUnitOfWork unitOfWork;

        public AddTrainerCommandHandler(
            IStudiosRepository studiosRepository,
            IUnitOfWork unitOfWork)
        {
            this.studiosRepository = studiosRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Success>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
        {
            Studio? studio = await studiosRepository.GetByIdAsync(request.StudioId);

            if (studio is null)
                return Error.NotFound(description: "Studio not found");

            var addTrainerResult = studio.AddTrainer(request.TrainerId);

            if (addTrainerResult.IsError)
                return addTrainerResult.Errors;

            await studiosRepository.UpdateAsync(studio);
            await unitOfWork.CommitChangesAsync();

            return Result.Success;
        }
    }
}
