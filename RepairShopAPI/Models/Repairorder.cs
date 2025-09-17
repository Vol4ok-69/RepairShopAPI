using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Repairorder
{
    public int Id { get; set; }

    public int Deviceid { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime Updatedat { get; set; }

    public DateTime? Expectedreadyat { get; set; }

    public int Statusid { get; set; }

    public decimal? Totalcost { get; set; }

    public string? Customerdescription { get; set; }

    public string? Diagnosticnotes { get; set; }

    public DateTime? Closedat { get; set; }

    public virtual Device Device { get; set; } = null!;

    public virtual ICollection<Orderassignment> Orderassignments { get; set; } = new List<Orderassignment>();

    public virtual ICollection<Orderpart> Orderparts { get; set; } = new List<Orderpart>();

    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Orderstatus Status { get; set; } = null!;
}
