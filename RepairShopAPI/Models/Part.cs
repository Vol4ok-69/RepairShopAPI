using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Part
{
    public int Id { get; set; }

    public string? Partsku { get; set; }

    public string Partname { get; set; } = null!;

    public int? Compatiblemodelid { get; set; }

    public int Stockqty { get; set; }

    public decimal Unitcost { get; set; }

    public virtual Devicemodel? Compatiblemodel { get; set; }

    public virtual ICollection<Orderpart> Orderparts { get; set; } = new List<Orderpart>();
}
