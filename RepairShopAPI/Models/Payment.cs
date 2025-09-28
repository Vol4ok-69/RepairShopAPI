﻿using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid? Invoiceid { get; set; }

    public DateTime Paidat { get; set; }

    public decimal Amount { get; set; }

    public string? Method { get; set; }

    public string? Reference { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
