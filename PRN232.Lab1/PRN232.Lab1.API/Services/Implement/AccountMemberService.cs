using Microsoft.EntityFrameworkCore;
using PRN232.Lab1.API.Payload.Request;
using PRN232.Lab1.API.Services.Interface;
using PRN232.Lab1.Domain.Entities;
using PRN232.Lab1.Repository.Interfaces;

namespace PRN232.Lab1.API.Services.Implement
{
	public class AccountMemberService : BaseService<AccountMemberService>, IAccountMemberService
	{
		public AccountMemberService(IUnitOfWork<DbContext> unitOfWork, ILogger<AccountMemberService> logger,
			IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, httpContextAccessor)
		{
		}

		public AccountMember GetAccountMember(AccountMemberRequest request)
		{
			AccountMember accountMember = _unitOfWork.GetRepository<AccountMember>()
				.SingleOrDefaultAsync(predicate: x => x.EmailAddress == request.EmailAddress && x.MemberPassword == request.MemberPassword).Result;
			return accountMember;
		}
	}
}
