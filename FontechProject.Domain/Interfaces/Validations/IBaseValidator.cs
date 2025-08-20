using FontechProject.Domain.Result;

namespace FontechProject.Domain.Interfaces.Validations;

public interface IBaseValidator<in T> where T: class
{
    BaseResult ValidateOnNull(T model);
}