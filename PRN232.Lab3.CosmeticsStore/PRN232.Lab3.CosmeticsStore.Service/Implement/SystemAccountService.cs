using PRN232.Lab3.CosmeticsStore.Repository.Entities;
using PRN232.Lab3.CosmeticsStore.Repository.Interfaces;
using PRN232.Lab3.CosmeticsStore.Service.Interface;
using PRN232.Lab3.CosmeticsStore.Service.Payload.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab3.CosmeticsStore.Service.Implement
{
	public class SystemAccountService : ISystemAccountService
	{
		private readonly IUnitOfWork _unitOfWork;
		public SystemAccountService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public Task<SystemAccount> Login(SystemAccountRequest request)
		{
			var account = _unitOfWork.GetRepository<SystemAccount>().SingleOrDefaultAsync(predicate: x => x.EmailAddress == request.EmailAddress && x.AccountPassword == request.AccountPassword).Result;
			if (account == null)
			{
				throw new Exception("Invalid username or password");
			}
			return Task.FromResult(account);
		}
	}
}
