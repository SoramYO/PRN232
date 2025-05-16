using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PRN232.Lab1.API.Services.Implement;
using PRN232.Lab1.API.Services.Interface;
using PRN232.Lab1.ProductStore.Repository.Entities;
using PRN232.Lab1.ProductStore.Repository.Implement;
using PRN232.Lab1.ProductStore.Repository.Interfaces;
using PRN232.Lab1.ProductStore.Service.Implement;
using PRN232.Lab1.ProductStore.Service.Interface;
using System.Text.Json.Serialization;

namespace PRN232.Lab1.ProductStore.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
			});

			// Configure Swagger
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Configure DbContext
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<MyStoreContext>(options =>
				options.UseSqlServer(connectionString,
					sqlServerOptions => sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
			);

			builder.Services.AddScoped<DbContext, MyStoreContext>();
			builder.Services.AddScoped<IUnitOfWork<MyStoreContext>, UnitOfWork<MyStoreContext>>();
			builder.Services.AddScoped<IUnitOfWork<DbContext>, UnitOfWork<DbContext>>();
			builder.Services.AddScoped<IAccountMemberService, AccountMemberService>();
			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<ICategoryService, CategoryService>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork<MyStoreContext>>();

			var app = builder.Build();

			// Apply migrations on startup
			using (var scope = app.Services.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<MyStoreContext>();
				try
				{
					dbContext.Database.Migrate(); // Apply pending migrations
				}
				catch (Exception ex)
				{
					var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while applying migrations.");
					throw; // Re-throw to ensure startup fails if migrations fail
				}
			}

			// Configure the HTTP request pipeline
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			// Comment out HTTPS redirection to avoid certificate issues
			// app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}
	}
}