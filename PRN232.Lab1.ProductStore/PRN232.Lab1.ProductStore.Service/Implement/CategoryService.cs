using PRN232.Lab1.ProductStore.Repository.Entities;
using PRN232.Lab1.ProductStore.Repository.Interfaces;
using PRN232.Lab1.ProductStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab1.ProductStore.Service.Implement
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<List<Category>> GetAllCategories()
		{
			var categories = await _unitOfWork.GetRepository<Category>().GetListAsync();
			return categories.ToList();
		}
	}
	

}
