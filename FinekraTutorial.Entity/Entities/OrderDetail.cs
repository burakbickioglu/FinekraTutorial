namespace FinekraTutorial.Entity.Entities;

public class OrderDetail :BaseEntity
{
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
    public Guid PerfumeId { get; set; }
    public virtual Perfume Perfume { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }
}
