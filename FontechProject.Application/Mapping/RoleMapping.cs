using AutoMapper;
using FontechProject.Application.Services;
using FontechProject.Domain.Dto.Role;
using FontechProject.Domain.Entity;

namespace FontechProject.Application.Mapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
    }
}