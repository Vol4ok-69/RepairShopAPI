using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Modelvariant
{
    public int Id { get; set; }

    public int Modelid { get; set; }

    public string Variantname { get; set; } = null!;

    public int? Processorid { get; set; }

    public int? Ramgb { get; set; }

    public int? Storagegb { get; set; }

    public string? Regioncode { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    public virtual Devicemodel Model { get; set; } = null!;

    public virtual Processor? Processor { get; set; }

    public virtual Region? RegioncodeNavigation { get; set; }
}
