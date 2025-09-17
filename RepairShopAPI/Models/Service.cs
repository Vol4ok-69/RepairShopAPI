using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Service
{
    public int Id { get; set; }

    public string? Servicecode { get; set; }

    public string Servicename { get; set; } = null!;

    public decimal Defaultprice { get; set; }

    public int? Laborminutes { get; set; }

    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();
}
