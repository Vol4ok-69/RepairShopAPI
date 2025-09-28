using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Orderpart
{
    public Guid Orderid { get; set; }

    public Guid Partid { get; set; }

    public int Quantity { get; set; }

    public decimal Priceattime { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Part Part { get; set; } = null!;
}
