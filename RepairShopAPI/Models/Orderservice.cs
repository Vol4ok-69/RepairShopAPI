using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Orderservice
{
    public Guid Orderid { get; set; }

    public Guid Serviceid { get; set; }

    public int Quantity { get; set; }

    public decimal Priceattime { get; set; }

    public string? Notes { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
