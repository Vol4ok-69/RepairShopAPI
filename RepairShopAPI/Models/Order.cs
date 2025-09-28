using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid Clientid { get; set; }

    public Guid Masterid { get; set; }

    public Guid Statusid { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Scheduledat { get; set; }

    public DateTime? Startedat { get; set; }

    public DateTime? Completedat { get; set; }

    public decimal? Totalprice { get; set; }

    public string? Externalreference { get; set; }

    public string? Notes { get; set; }

    public virtual User Client { get; set; } = null!;

    public virtual Invoice? Invoice { get; set; }

    public virtual User Master { get; set; } = null!;

    public virtual ICollection<Orderevent> Orderevents { get; set; } = new List<Orderevent>();

    public virtual ICollection<Orderpart> Orderparts { get; set; } = new List<Orderpart>();

    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();

    public virtual Orderstatus Status { get; set; } = null!;
}
