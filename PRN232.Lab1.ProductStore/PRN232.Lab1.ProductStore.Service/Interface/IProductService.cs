using PRN232.Lab1.ProductStore.Repository.Entities;
using PRN232.Lab1.ProductStore.Repository.Paginate;
using PRN232.Lab1.ProductStore.Service.Payload.Request;

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
