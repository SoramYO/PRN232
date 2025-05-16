﻿using Microsoft.AspNetCore.Mvc;
using PRN232.Lab1.ProductStore.Service.Interface;

namespace PRN232.Lab1.ProductStore.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await _categoryService.GetAllCategories();
			return Ok(categories);
		}
	}
}
