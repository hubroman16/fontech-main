using FontechProject.Domain.Interfaces;

namespace FontechProject.Domain.Entity;

public class User : IEntityId<long>, IAuditable
{
    public long Id { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public UserToken UserToken { get; set; }
    
    public DateTime CreateAt { get; set; }
    
    public List<Report> Reports { get; set; }
    
    public List<Role> Roles { get; set; }
    
    public DateTime? UpdateAt { get; set; }
    
    
}