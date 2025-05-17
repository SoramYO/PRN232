using PRN232.Lab3.CosmeticsStore.Repository.Entities;
using PRN232.Lab3.CosmeticsStore.Service.Payload.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab3.CosmeticsStore.Service.Interface
{
	public interface ISystemAccountService
	{
		Task<SystemAccount> Login(SystemAccountRequest request);
	}
}
