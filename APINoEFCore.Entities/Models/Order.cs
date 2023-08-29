namespace APINoEFCore.Entities.Models;
public record Order : IDefaultEntity
{
    public Guid PersonId { get; set; }
    public virtual Person Person { get; set;}
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set;}
    public int Qty { get; set; }
    public decimal Total { get; set; }
}
