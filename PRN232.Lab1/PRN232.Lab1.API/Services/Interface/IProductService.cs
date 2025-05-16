using PRN232.Lab1.API.Payload.Request;
using PRN232.Lab1.Domain.Entities;
using PRN232.Lab1.Domain.Paginate;

namespace PRN232.Lab1.API.Services.Interface
{
	public interface IProductService
	{
		Task AddProduct(ProductRequest request);

		Task UpdateProduct(ProductRequest request);

		Task DeleteProduct(ProductRequest request);

		Task<IPaginate<Product>> GetAllProducts(string? search, int page, int size);

		Task<Product> GetProductById(int productId);
	}
}
