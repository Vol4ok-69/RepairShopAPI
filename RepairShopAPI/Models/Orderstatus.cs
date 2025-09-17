using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Orderstatus
{
    public int Id { get; set; }

    public string Statuscode { get; set; } = null!;

    public string Displayname { get; set; } = null!;

    public virtual ICollection<Repairorder> Repairorders { get; set; } = new List<Repairorder>();
}
