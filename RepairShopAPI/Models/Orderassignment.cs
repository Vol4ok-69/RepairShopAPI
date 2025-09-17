using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Orderassignment
{
    public int Id { get; set; }

    public int Orderid { get; set; }

    public int Employeeid { get; set; }

    public DateTime Assignedat { get; set; }

    public string? Role { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Repairorder Order { get; set; } = null!;
}
