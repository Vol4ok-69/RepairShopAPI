using System;
using System.Collections.Generic;

namespace RepairShopAPI.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }

    public DateOnly? Hiredat { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Orderassignment> Orderassignments { get; set; } = new List<Orderassignment>();
}
