namespace FontechProject.Domain.Dto.Role;

public class UpdateUserRoleDto
{
    public string Login { get; set; }
    
    public long FromRoleId { get; set; }
    
    public long ToRoleId { get; set; }
}