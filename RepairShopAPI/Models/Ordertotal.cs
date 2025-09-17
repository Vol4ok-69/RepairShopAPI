using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Ordertotal
{
    public int? Orderid { get; set; }

    public DateTime? Createdat { get; set; }

    public decimal? Servicestotal { get; set; }

    public decimal? Partstotal { get; set; }

    public decimal? Grandtotal { get; set; }
}
