using FontechProject.Domain.Interfaces;

namespace FontechProject.Domain.Entity;

public class Role : IEntityId<long>
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public List<User> Users { get; set; }
}