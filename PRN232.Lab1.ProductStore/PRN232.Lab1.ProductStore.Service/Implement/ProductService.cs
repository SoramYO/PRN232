using Microsoft.EntityFrameworkCore;

using PRN232.Lab1.API.Services.Interface;
using PRN232.Lab1.ProductStore.Repository.Entities;
using PRN232.Lab1.ProductStore.Repository.Interfaces;
using PRN232.Lab1.ProductStore.Repository.Paginate;
using PRN232.Lab1.ProductStore.Service.Payload.Request;


namespace PRN232.Lab1.API.Services.Implement
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task AddProduct(ProductRequest request)
		{
			var product = new Product
			{
				ProductId = request.ProductId,
				ProductName = request.ProductName,
				CategoryId = request.CategoryId,
				UnitsInStock = request.UnitsInStock,
				UnitPrice = request.UnitPrice,
			};

			await _unitOfWork.GetRepository<Product>().InsertAsync(product);
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateProduct(ProductRequest request)
		{
			var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(predicate: x => x.ProductId == request.ProductId);
			if (product == null)
				throw new Exception("Product not found.");

			product.ProductName = request.ProductName;
			product.CategoryId = request.CategoryId;
			product.UnitsInStock = request.UnitsInStock;
			product.UnitPrice = request.UnitPrice;
			// Set other fields as needed

			_unitOfWork.GetRepository<Product>().UpdateAsync(product);
			await _unitOfWork.CommitAsync();
		}

		public async Task DeleteProduct(ProductRequest request)
		{
			var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(predicate: x => x.ProductId == request.ProductId);
			if (product == null)
				throw new Exception("Product not found.");

			_unitOfWork.GetRepository<Product>().DeleteAsync(product);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IPaginate<Product>> GetAllProducts(string? search, int page, int size)
		{
			return await _unitOfWork.GetRepository<Product>()
				.GetPagingListAsync(
					predicate: x => string.IsNullOrEmpty(search) || x.ProductName.Contains(search),
					include: x => x.Include(y => y.Category),
					page: page,
					size: size
				);
		}

		public async Task<Product> GetProductById(int productId)
		{
			return await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(predicate: x => x.ProductId == productId);
		}
	}

}
