using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Devicebrand
{
    public int Id { get; set; }

    public string Brandname { get; set; } = null!;

    public virtual ICollection<Devicemodel> Devicemodels { get; set; } = new List<Devicemodel>();
}
