using Common.DTOs;
using AutoMapper;
using Infrastructure.Entities;

namespace Application.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>();
            CreateMap<CreateEventDTO, Event>();
            CreateMap<UpdateEventDTO, Event>();

            CreateMap<Participant, ParticipantDTO>();
            CreateMap<RegisterParticipantDTO, Participant>();
        }
    }
}