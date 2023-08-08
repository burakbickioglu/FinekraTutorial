namespace FinekraTutorial.Entity.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Perfume>? Perfumes { get; set; }
}
