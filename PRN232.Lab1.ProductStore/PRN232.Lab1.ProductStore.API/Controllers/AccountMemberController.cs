
using Microsoft.AspNetCore.Mvc;
using PRN232.Lab1.API.Services.Interface;
using PRN232.Lab1.ProductStore.Service.Payload.Request;

namespace PRN232.Lab1.ProductStore.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountMemberController : ControllerBase
	{
		private readonly IAccountMemberService _accountMemberService;

		public AccountMemberController(IAccountMemberService accountMemberService)
		{
			_accountMemberService = accountMemberService;
		}
		[HttpPost("login")]
		public IActionResult Login( [FromBody] AccountMemberRequest request)
		{
			var accountMember = _accountMemberService.GetAccountMember(request);
			if (accountMember == null)
			{
				return Unauthorized();
			}
			return Ok(accountMember);
		}
	}
}
