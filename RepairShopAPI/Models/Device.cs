using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Device
{
    public int Id { get; set; }

    public int Clientid { get; set; }

    public int Modelid { get; set; }

    public int? Variantid { get; set; }

    public string? Imeiorserial { get; set; }

    public DateOnly? Purchasedate { get; set; }

    public string? Notes { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Devicemodel Model { get; set; } = null!;

    public virtual ICollection<Repairorder> Repairorders { get; set; } = new List<Repairorder>();

    public virtual Modelvariant? Variant { get; set; }
}
