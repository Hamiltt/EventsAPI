using Common.Exceptions;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UseCases.Events
{
    public class DeleteEventUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventUseCase(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid event id.");
            }

            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with id {id} not found.");
            }

            _eventRepository.Remove(eventEntity);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}