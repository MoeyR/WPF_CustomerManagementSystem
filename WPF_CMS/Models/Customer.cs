using System;
using System.Collections.Generic;

namespace WPF_CMS.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string IdNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
