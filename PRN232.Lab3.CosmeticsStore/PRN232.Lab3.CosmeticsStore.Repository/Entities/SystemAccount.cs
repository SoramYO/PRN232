using System;
using System.Collections.Generic;

namespace PRN232.Lab3.CosmeticsStore.Repository.Entities;

public partial class SystemAccount
{
    public Guid AccountId { get; set; }

    public string? AccountNote { get; set; }

    public string? AccountPassword { get; set; }

    public string? EmailAddress { get; set; }

    public int? Role { get; set; }
}
