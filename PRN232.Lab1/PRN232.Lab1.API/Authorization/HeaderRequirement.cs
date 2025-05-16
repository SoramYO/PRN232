using Microsoft.AspNetCore.Authorization;

namespace PRN232.Lab1.API.Authorization
{
	public class HeaderRequirement : IAuthorizationRequirement
	{
		public string HeaderName { get; }
		public string HeaderValue { get; }

		public HeaderRequirement(string headerName, string headerValue = null)
		{
			HeaderName = headerName;
			HeaderValue = headerValue;
		}
	}
}
