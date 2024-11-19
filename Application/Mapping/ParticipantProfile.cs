using AutoMapper;
using Common.DTOs;
using Domain.Entities;

namespace Application.Mapping
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<Participant, ParticipantDTO>();
            CreateMap<RegisterParticipantDTO, Participant>();
        }
    }
}