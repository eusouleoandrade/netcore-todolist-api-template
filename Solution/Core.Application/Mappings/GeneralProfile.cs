using AutoMapper;
using Core.Application.Dtos.Queries;
using Core.Domain.Entities;

namespace Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Todo, TodoQuery>();
        }
    }
}
