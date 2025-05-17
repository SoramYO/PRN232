using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN232.Lab3.CosmeticsStore.Repository.Entities;

public partial class CosmeticInformation
{
	[Key]
	public string CosmeticId { get; set; } = null!;

    public string? CategoryId { get; set; }

    public string? CosmeticName { get; set; }

    public string? CosmeticSize { get; set; }

    public decimal? DollarPrice { get; set; }

    public string? ExpirationDate { get; set; }

    public string? SkinType { get; set; }

    public virtual CosmeticCategory? Category { get; set; }
}
