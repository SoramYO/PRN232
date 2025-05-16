
using PRN232.Lab1.ProductStore.Repository.Entities;
using PRN232.Lab1.ProductStore.Service.Payload.Request;

namespace PRN232.Lab1.API.Services.Interface
{
	public interface IAccountMemberService
	{
		AccountMember GetAccountMember(AccountMemberRequest request);
	}
}
