using AutoMapper;
using FontechProject.Domain.Dto.Report;
using FontechProject.Domain.Entity;

namespace FontechProject.Application.Mapping;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        //CreateMap<Report, ReportDto>()
          //  .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
            //.ForCtorParam(ctorParamName: "Name", m => m.MapFrom(s => s.Name))
            //.ForCtorParam(ctorParamName: "Description", m => m.MapFrom(s => s.Description))
            //.ForCtorParam(ctorParamName: "DateCreated", m => m.MapFrom(s => s.CreateAt))
            //.ReverseMap();
            CreateMap<Report, ReportDto>()
                .ConstructUsing((src, ctx) => new ReportDto(
                    Id: src.Id,
                    Name: src.Name,
                    Description: src.Description,
                    DataCreated: src.CreateAt.ToString("yyyy-MM-dd")
                ))
                .ReverseMap();
    }
}