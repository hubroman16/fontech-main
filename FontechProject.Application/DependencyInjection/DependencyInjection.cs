using FluentValidation;
using FontechProject.Application.Mapping;
using FontechProject.Application.Services;
using FontechProject.Application.Validations;
using FontechProject.Application.Validations.FluentValidations.Report;
using FontechProject.Application.Validations.FluentValidations.Role;
using FontechProject.Application.Validations.FluentValidations.User;
using FontechProject.Domain.Dto.Report;
using FontechProject.Domain.Entity;
using FontechProject.Domain.Interfaces.Services;
using FontechProject.Domain.Interfaces.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace FontechProject.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ReportMapping));
        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IRoleValidator, RoleValidator>();
        services.AddScoped<IBaseValidator<User>, UserValidator>();
        services.AddScoped<IReportValidator, ReportValidator>();
        services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
        services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}