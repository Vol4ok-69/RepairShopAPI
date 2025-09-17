using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Region
{
    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Modelvariant> Modelvariants { get; set; } = new List<Modelvariant>();
}
