using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Devicemodel
{
    public int Id { get; set; }

    public int Brandid { get; set; }

    public string Modelname { get; set; } = null!;

    public virtual Devicebrand Brand { get; set; } = null!;

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    public virtual ICollection<Modelvariant> Modelvariants { get; set; } = new List<Modelvariant>();

    public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
}
