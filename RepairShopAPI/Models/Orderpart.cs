using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Orderpart
{
    public int Id { get; set; }

    public int Orderid { get; set; }

    public int Partid { get; set; }

    public int Quantity { get; set; }

    public decimal Unitprice { get; set; }

    public string? Notes { get; set; }

    public virtual Repairorder Order { get; set; } = null!;

    public virtual Part Part { get; set; } = null!;
}
