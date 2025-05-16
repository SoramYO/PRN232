using Microsoft.EntityFrameworkCore;
using PRN232.Lab1.Repository.Interfaces;

namespace PRN232.Lab1.API.Services
{
	public abstract class BaseService<T> where T : class
	{
		protected IUnitOfWork<DbContext> _unitOfWork;
		protected ILogger<T> _logger;

		protected IHttpContextAccessor _httpContextAccessor;

		public BaseService(IUnitOfWork<DbContext> unitOfWork, ILogger<T> logger,
			IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
		}
	}
}
