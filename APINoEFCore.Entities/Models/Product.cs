namespace APINoEFCore.Entities.Models;
public record Product : IDefaultEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
