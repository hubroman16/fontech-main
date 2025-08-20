using FontechProject.Domain.Interfaces;

namespace FontechProject.Domain.Entity;

public class Report : IEntityId<long>, IAuditable
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreateAt { get; set; }
    
    public User User { get; set; }
    
    public long UserId { get; set; }
    
    public DateTime? UpdateAt { get; set; }
    
    
}