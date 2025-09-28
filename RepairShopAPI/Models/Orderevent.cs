using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Orderevent
{
    public Guid Id { get; set; }

    public Guid Orderid { get; set; }

    public DateTime Createdat { get; set; }

    public Guid? Actoruserid { get; set; }

    public string Eventtype { get; set; } = null!;

    public string? Eventdata { get; set; }

    public virtual User? Actoruser { get; set; }

    public virtual Order Order { get; set; } = null!;
}
