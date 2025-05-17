using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab3.CosmeticsStore.Service.Payload.Request
{
    public class CosmeticInformationRequest
    {
		public string CosmeticId { get; set; } = null!;
		public string? CategoryId { get; set; }
		public string? CosmeticName { get; set; }
		public string? CosmeticSize { get; set; }
		public decimal? DollarPrice { get; set; }
		public string? ExpirationDate { get; set; }
		public string? SkinType { get; set; }
	}
}
