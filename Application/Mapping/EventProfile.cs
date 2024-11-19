using Common.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>();
            CreateMap<CreateEventDTO, Event>();
            CreateMap<UpdateEventDTO, Event>();
        }
    }
}