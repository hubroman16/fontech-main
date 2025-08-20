using FontechProject.Domain.Entity;
using Microsoft.EntityFrameworkCore.Storage;

namespace FontechProject.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IStateSaveChanges
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    
    IBaseRepository<User> Users { get; set; } 
    
    IBaseRepository<Role> Roles { get; set; }
    
    IBaseRepository<UserRole> UserRoles { get; set; }
}