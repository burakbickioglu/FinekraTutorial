namespace FinekraTutorial.Entity.Interfaces;

public interface IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
}
