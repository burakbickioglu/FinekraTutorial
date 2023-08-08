namespace FinekraTutorial.Entity.Entities;

public class Perfume : BaseEntity
{
    public string Name { get; set; }
    public Guid BrandId { get; set; }
    public virtual Brand Brand { get; set; }
    public decimal Price { get; set; }
    public string PhotoPath { get; set; }
    public virtual ICollection<OrderDetail> OrderDetais { get; set; }
}
