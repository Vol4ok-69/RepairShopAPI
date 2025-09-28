using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Part
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Sku { get; set; }

    public string? Description { get; set; }

    public decimal Unitprice { get; set; }

    public int Stockquantity { get; set; }

    public virtual ICollection<Orderpart> Orderparts { get; set; } = new List<Orderpart>();
}
