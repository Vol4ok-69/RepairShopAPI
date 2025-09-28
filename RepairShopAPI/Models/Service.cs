using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Service
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? Durationminutes { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();
}
