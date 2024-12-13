using Common.Exceptions;
using Domain.UnitOfWork;

namespace Application.UseCases.Events
{
    public class DeleteEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid event id.");
            }

            var eventEntity = await _unitOfWork.Events.GetByIdAsync(id);
            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with id {id} not found.");
            }

            _unitOfWork.Events.Remove(eventEntity);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}