using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Orderservice
{
    public int Id { get; set; }

    public int Orderid { get; set; }

    public int Serviceid { get; set; }

    public int Quantity { get; set; }

    public decimal Unitprice { get; set; }

    public string? Notes { get; set; }

    public virtual Repairorder Order { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
