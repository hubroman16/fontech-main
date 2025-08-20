using FluentValidation;
using FontechProject.Domain.Dto.Role;

namespace FontechProject.Application.Validations.FluentValidations.Role;

public class UpdateRoleValidator : AbstractValidator<UpdateUserRoleDto>
{
    public UpdateRoleValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
    }
}