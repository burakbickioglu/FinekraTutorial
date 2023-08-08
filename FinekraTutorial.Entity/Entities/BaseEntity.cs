using FinekraTutorial.Entity.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FinekraTutorial.Entity.Entities;

public class BaseEntity : IBaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
}
