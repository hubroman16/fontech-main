namespace FontechProject.Domain.Interfaces.Repositories;

public interface IStateSaveChanges
{
    Task<int> SaveChangesAsync();
}