
using Microsoft.AspNetCore.Mvc;
using PRN232.Lab1.API.Payload.Request;
using PRN232.Lab1.API.Services.Interface;

namespace PRN232.Lab1.API.Controllers
{
	public class AccountMemberController : BaseController<AccountMemberController>
	{
		private readonly IAccountMemberService _accountMemberService;

		public AccountMemberController(IAccountMemberService accountMemberService ,ILogger<AccountMemberController> logger) : base(logger)
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
