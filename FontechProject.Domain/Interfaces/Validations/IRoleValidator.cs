using FontechProject.Domain.Entity;
using FontechProject.Domain.Result;

namespace FontechProject.Domain.Interfaces.Validations;

public interface IRoleValidator : IBaseValidator<Role>
{ 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="role"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult SearchValidator(Role role, User user);
    
}