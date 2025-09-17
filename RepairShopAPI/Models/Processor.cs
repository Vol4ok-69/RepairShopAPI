using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Processor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Cores { get; set; }

    public decimal? Baseclockghz { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Modelvariant> Modelvariants { get; set; } = new List<Modelvariant>();
}
