using AutoMapper;
using Todo.Service.DataTransferObjects;

namespace Todo.Service.AutoMapperConfiguration;
public class TodoMappingProfile : Profile
{
    public TodoMappingProfile()
    {
        CreateMap<Core.Entities.Todo , TodoForCreatinDto>().ReverseMap();
        CreateMap<Core.Entities.Todo , TodoForUpdatingDto>().ReverseMap();
        CreateMap<Core.Entities.Todo , TodoDto>()
            .ForMember(dest => dest.Status ,
                opt => opt.MapFrom(src => src.Status.ToString())
            )
            .ForMember(dest => dest.Priority ,
                opt => opt.MapFrom(src => src.Priority.ToString())
            );
    }
}