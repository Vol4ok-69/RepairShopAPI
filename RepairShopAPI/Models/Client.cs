using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime Createdat { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
