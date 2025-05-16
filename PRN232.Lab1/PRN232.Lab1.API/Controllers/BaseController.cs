using Microsoft.AspNetCore.Mvc;

namespace PRN232.Lab1.API.Controllers
{
	[Route("/api/v1")]
	[ApiController]
	public class BaseController<T> : ControllerBase where T : BaseController<T>
	{
		protected ILogger<T> _logger;

		public BaseController(ILogger<T> logger)
		{
			_logger = logger;
		}
	}
}
