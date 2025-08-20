using AutoMapper;
using FontechProject.Application.Resources;
using FontechProject.Domain.Dto.Role;
using FontechProject.Domain.Entity;
using FontechProject.Domain.Enum;
using FontechProject.Domain.Interfaces.Repositories;
using FontechProject.Domain.Interfaces.Services;
using FontechProject.Domain.Interfaces.Validations;
using FontechProject.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace FontechProject.Application.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly IBaseRepository<UserRole> _userRoleRepository;
    private readonly IMapper _mapper;
    private readonly IRoleValidator _roleValidator;
    private readonly IBaseValidator<User> _userValidator;

    public RoleService(IBaseRepository<User> userRepository, IBaseRepository<Role> roleRepository, IMapper mapper, 
        IBaseRepository<UserRole> userRoleRepository, IUnitOfWork unitOfWork, IRoleValidator roleValidator, IBaseValidator<User> userValidator)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
        _unitOfWork = unitOfWork;
        _roleValidator = roleValidator;
        _userValidator = userValidator;
    }

    public async Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
        if (role != null)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleAlreadyExists,
                ErrorCode = (int)ErrorCodes.RoleAlreadyExists
            };
        }

        role = new Role()
        {
            Name = dto.Name
        };
        await _roleRepository.CreateAsync(role);
        await _roleRepository.SaveChangesAsync();
        return new BaseResult<RoleDto>()
        {
            Data = _mapper.Map<RoleDto>(role)
        };
    }

    public async Task<BaseResult<RoleDto>> DeleteRoleAsync(long id)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        var result = _roleValidator.ValidateOnNull(role);
        if (!result.IsSuccess)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        _roleRepository.Remove(role);
        await _roleRepository.SaveChangesAsync();
        return new BaseResult<RoleDto>()
        {
            Data = _mapper.Map<RoleDto>(role)
        };
    }

    public async Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
        
        var result = _roleValidator.ValidateOnNull(role);
        if (!result.IsSuccess)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        role.Name = dto.Name;
        var updatedRole = _roleRepository.Update(role);
        await _roleRepository.SaveChangesAsync();
        return new BaseResult<RoleDto>()
        {
            Data = _mapper.Map<RoleDto>(updatedRole)
        };
    }

    public async Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto)
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);
        var result = _userValidator.ValidateOnNull(user);
        if (!result.IsSuccess)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        var roles = user.Roles.Select(x => x.Name).ToArray();
        if (roles.All(x => x != dto.RoleName))
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.RoleName);
            var result2 = _roleValidator.ValidateOnNull(role);
            if (!result2.IsSuccess)
            {
                return new BaseResult<UserRoleDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }
            

            UserRole userRole = new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            await _userRoleRepository.CreateAsync(userRole);
            await _userRoleRepository.SaveChangesAsync();

            return new BaseResult<UserRoleDto>()
            {
                Data = new UserRoleDto()
                {
                    Login = user.Login,
                    RoleName = role.Name
                }
            };
        }

        return new BaseResult<UserRoleDto>()
        {
            ErrorMessage = ErrorMessage.UserAlreadyExistsThisRole,
            ErrorCode = (int)ErrorCodes.UserAlreadyExistsThisRole
        };
    }

    public async Task<BaseResult<UserRoleDto>> DeleteRoleForUserAsync(DeleteUserRoleDto dto)
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);
        if (user == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }
        
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.RoleId);
        if (role == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }

        var userRole = await _userRoleRepository.GetAll()
            .Where(x => x.RoleId == role.Id)
            .FirstOrDefaultAsync(x => x.UserId == user.Id);
        
        _userRoleRepository.Remove(userRole);
        await _userRoleRepository.SaveChangesAsync();
        
        return new BaseResult<UserRoleDto>()
        {
            Data = new UserRoleDto()
            {
                Login = user.Login,
                RoleName = role.Name
            }
        };
    }

    public async Task<BaseResult<UserRoleDto>> UpdateRoleForUserAsync(UpdateUserRoleDto dto)
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);
        
        var role = user.Roles.FirstOrDefault(x => x.Id == dto.FromRoleId);
        var result = _roleValidator.SearchValidator(role, user);
        if (!result.IsSuccess)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }
        var newRoleForUser = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.ToRoleId);
        var result2 = _roleValidator.ValidateOnNull(newRoleForUser);
        if (!result2.IsSuccess)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }
        
        using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                var userRole = await _unitOfWork.UserRoles.GetAll()
                    .Where(x => x.RoleId == role.Id)
                    .FirstAsync(x => x.UserId == user.Id);
                
                _unitOfWork.UserRoles.Remove(userRole);
                await _unitOfWork.SaveChangesAsync();

                var newUserRole = new UserRole()
                {
                    UserId = user.Id,
                    RoleId = newRoleForUser.Id
                };

                await _unitOfWork.UserRoles.CreateAsync(newUserRole);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
        
        return new BaseResult<UserRoleDto>()
        {
            Data = new UserRoleDto()
            {
                Login = user.Login,
                RoleName = newRoleForUser.Name
            }
        };
    }
}