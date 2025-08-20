using AutoMapper;
using FontechProject.Domain.Dto.User;
using FontechProject.Domain.Entity;

namespace FontechProject.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}