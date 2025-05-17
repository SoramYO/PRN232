using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab3.CosmeticsStore.Service.Payload.Response
{
    public class SystemAccountResponse
    {
		public string Token { get; set; }
		public string Role { get; set; }
		public string AccountId { get; set; }
	}
}
