using FontechProject.Application.Resources;
using FontechProject.Domain.Enum;
using FontechProject.Domain.Interfaces.Validations;
using FontechProject.Domain.Result;
using FontechProject.Domain.Entity;

namespace FontechProject.Application.Validations.FluentValidations.Role;

public class RoleValidator : IRoleValidator
{
    public BaseResult ValidateOnNull(Domain.Entity.Role model)
    {
        if (model == null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }

        return new BaseResult();
    }

    public BaseResult SearchValidator(Domain.Entity.Role role, Domain.Entity.User user)
    {
        if (role == null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }

        if (user == null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }

        return new BaseResult();
    }
}