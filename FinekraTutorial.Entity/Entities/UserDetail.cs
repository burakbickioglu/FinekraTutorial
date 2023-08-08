﻿namespace FinekraTutorial.Entity.Entities;

public class UserDetail : BaseEntity
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
}
