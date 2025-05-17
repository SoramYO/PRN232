using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab3.CosmeticsStore.Service.Payload.Request
{
   public class SystemAccountRequest
    {
        public string EmailAddress { get; set; } 
		public string AccountPassword { get; set; }
	}
}
