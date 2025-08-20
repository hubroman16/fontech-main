using FontechProject.Application.Resources;
using FontechProject.Domain.Enum;
using FontechProject.Domain.Interfaces.Validations;
using FontechProject.Domain.Result;

namespace FontechProject.Application.Validations.FluentValidations.User;

public class UserValidator : IBaseValidator<Domain.Entity.User>
{
    public BaseResult ValidateOnNull(Domain.Entity.User user)
    {
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