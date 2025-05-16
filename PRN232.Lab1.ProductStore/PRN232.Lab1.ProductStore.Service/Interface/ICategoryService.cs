using PRN232.Lab1.ProductStore.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab1.ProductStore.Service.Interface
{
	public interface ICategoryService
    {
		Task<List<Category>> GetAllCategories();
	}
}
