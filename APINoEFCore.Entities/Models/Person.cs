namespace APINoEFCore.Entities.Models;
public record Person : IDefaultEntity
{
    public string Name { get; set; }
    public string Email { get; set;}
    public string PasswordHash { get; set; }
    public string Salt { get; set; } 
    public bool IsAdmin { get; set;}
}
