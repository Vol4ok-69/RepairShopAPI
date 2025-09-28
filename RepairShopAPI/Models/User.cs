using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class User
{
    public Guid Id { get; set; }

    public Guid Roleid { get; set; }

    public string? Email { get; set; }

    public string? Passwordhash { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Phone { get; set; }

    public DateTime Createdat { get; set; }

    public bool Isactive { get; set; }

    public string Patronymic { get; set; } = null!;





    public virtual Customerprofile? Customerprofile { get; set; }

    public virtual ICollection<Order> OrderClients { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderMasters { get; set; } = new List<Order>();

    public virtual ICollection<Orderevent> Orderevents { get; set; } = new List<Orderevent>();

    public virtual Role Role { get; set; } = null!;

    public virtual Technicianprofile? Technicianprofile { get; set; }
}
