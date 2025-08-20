using FluentValidation;
using FontechProject.Domain.Dto.Role;

namespace FontechProject.Application.Validations.FluentValidations.Role;

public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}