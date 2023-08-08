namespace FinekraTutorial.Entity.Entities;

public class Order : BaseEntity
{
    public Guid UserDetailId { get; set; }
    public virtual UserDetail UserDetail { get; set; }
    public string ShipAddress { get; set; }
    public string Phone { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsCompleted { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }

}
