using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Technicianprofile
{
    public Guid Userid { get; set; }

    public string? Employeenumber { get; set; }

    public DateOnly? Hiredate { get; set; }

    public decimal? Hourlyrate { get; set; }

    public string? Certifications { get; set; }

    public bool Isavailable { get; set; }

    public virtual User User { get; set; } = null!;
}
