using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int Orderid { get; set; }

    public DateTime Paidat { get; set; }

    public decimal Amount { get; set; }

    public string? Method { get; set; }

    public string? Notes { get; set; }

    public virtual Repairorder Order { get; set; } = null!;
}
