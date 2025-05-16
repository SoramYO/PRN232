using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PRN232.Lab1.API.Services.Implement;
using PRN232.Lab1.API.Services.Interface;
using PRN232.Lab1.Domain.Entities;
using PRN232.Lab1.Repository.Implement;
using PRN232.Lab1.Repository.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PRN232.Lab1.API.Extensions
{
	public static class DependencyServices
	{
		public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork<MyStoreContext>, UnitOfWork<MyStoreContext>>();
			services.AddScoped<IUnitOfWork<DbContext>, UnitOfWork<DbContext>>();

			return services;
		}

		public static IServiceCollection AddDatabase(this IServiceCollection services)
		{
			IConfiguration configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

			services.AddDbContext<MyStoreContext>(options =>
				options.UseSqlServer(CreateConnectionString(configuration),
					sqlServerOptions => sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
			);

			services.AddScoped<DbContext>(provider => provider.GetService<MyStoreContext>());
			return services;
		}


		private static string CreateConnectionString(IConfiguration configuration)
		{
			var connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
			return connectionString;
		}


		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IAccountMemberService, AccountMemberService>();
			services.AddScoped<IProductService, ProductService>();


			return services;
		}


		public static IServiceCollection AddConfigSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo()
				{
					Title = "PRN232.Lab1",
					Version = "v1"
				});
				options.MapType<TimeOnly>(() => new OpenApiSchema
				{
					Type = "string",
					Format = "time",
					Example = OpenApiAnyFactory.CreateFromJson("\"13:45:42.0000000\"")
				});
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					In = ParameterLocation.Header,
					Description = "Please enter a valid token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] { }
				}
			});
			});
			return services;
		}

	}
}
