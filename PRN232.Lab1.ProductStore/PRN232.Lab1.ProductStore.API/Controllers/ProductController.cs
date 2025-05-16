using Microsoft.AspNetCore.Mvc;
using PRN232.Lab1.API.Services.Interface;
using PRN232.Lab1.ProductStore.Service.Payload.Request;

namespace PRN232.Lab1.ProductStore.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProducts(string? search, int page = 1, int size = 10)
		{
			var products = await _productService.GetAllProducts(search, page, size);
			return Ok(products);
		}

		[HttpGet("products/{productId}")]
		public async Task<IActionResult> GetProductById(int productId)
		{
			var product = await _productService.GetProductById(productId);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct([FromBody] ProductRequest request)
		{
			if (request == null)
				return BadRequest();

			await _productService.AddProduct(request);

			return CreatedAtAction(nameof(GetProductById), new { productId = request.ProductId }, request);
		}

		[HttpPut("products/{productId}")]
		public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductRequest product)
		{
			if (product == null || productId != product.ProductId)
			{
				return BadRequest();
			}

			var existingProduct = await _productService.GetProductById(productId);
			if (existingProduct == null)
			{
				return NotFound();
			}

			await _productService.UpdateProduct(product);
			return NoContent();
		}

		[HttpDelete("products/{productId}")]
		public async Task<IActionResult> DeleteProduct(int productId)
		{
			var product = await _productService.GetProductById(productId);
			if (product == null)
			{
				return NotFound();
			}

			await _productService.DeleteProduct(new ProductRequest { ProductId = productId });
			return NoContent();
		}
	}
}
