using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Invoice
{
    public Guid Id { get; set; }

    public Guid Orderid { get; set; }

    public DateTime Issuedat { get; set; }

    public DateTime? Dueat { get; set; }

    public decimal Totalamount { get; set; }

    public decimal Paidamount { get; set; }

    public bool Ispaid { get; set; }

    public string? Metadata { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
