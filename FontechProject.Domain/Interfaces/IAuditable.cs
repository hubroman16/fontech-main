namespace FontechProject.Domain.Interfaces;

public interface IAuditable
{
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}