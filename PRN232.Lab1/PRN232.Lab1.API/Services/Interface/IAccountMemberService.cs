using PRN232.Lab1.API.Payload.Request;
using PRN232.Lab1.Domain.Entities;

namespace PRN232.Lab1.API.Services.Interface
{
	public interface IAccountMemberService
	{
		AccountMember GetAccountMember(AccountMemberRequest request);
	}
}
