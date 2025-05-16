using Microsoft.EntityFrameworkCore;

using PRN232.Lab1.API.Services.Interface;
using PRN232.Lab1.ProductStore.Repository.Entities;
using PRN232.Lab1.ProductStore.Repository.Interfaces;
using PRN232.Lab1.ProductStore.Service.Payload.Request;

namespace PRN232.Lab1.ProductStore.Service.Implement
{
	public class AccountMemberService :  IAccountMemberService
	{
		private readonly IUnitOfWork _unitOfWork;
		public AccountMemberService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public AccountMember GetAccountMember(AccountMemberRequest request)
		{
			AccountMember accountMember = _unitOfWork.GetRepository<AccountMember>()
				.SingleOrDefaultAsync(predicate: x => x.EmailAddress == request.EmailAddress && x.MemberPassword == request.MemberPassword).Result;
			return accountMember;
		}
	}
}
