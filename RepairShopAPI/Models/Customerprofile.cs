using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Customerprofile
{
    public Guid Userid { get; set; }

    public string? Address { get; set; }

    public string? Preferredcontactmethod { get; set; }

    public string? Notes { get; set; }

    public virtual User User { get; set; } = null!;
}
