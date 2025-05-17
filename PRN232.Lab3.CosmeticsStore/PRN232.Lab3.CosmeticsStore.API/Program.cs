
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using PRN232.Lab3.CosmeticsStore.Repository.Entities;
using PRN232.Lab3.CosmeticsStore.Repository.Implement;
using PRN232.Lab3.CosmeticsStore.Repository.Interfaces;
using PRN232.Lab3.CosmeticsStore.Service.Implement;
using PRN232.Lab3.CosmeticsStore.Service.Interface;
using System.Text;
using System.Text.Json.Serialization;

namespace PRN232.Lab3.CosmeticsStore.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var modelBuilder = new ODataConventionModelBuilder();
			modelBuilder.EntitySet<CosmeticInformation>("CosmeticInformations");
			modelBuilder.EntitySet<CosmeticCategory>("CosmeticCategories");
			builder.Services.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
					options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
				})
				.AddOData(
						options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents("odata", modelBuilder.GetEdmModel())
						);


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
			builder.Services.AddDbContext<CosmeticsDbContext>(options =>
				options.UseSqlServer(connectionString,
					sqlServerOptions => sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
			);
			IConfiguration configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true).Build();


			builder.Services.AddScoped<DbContext, CosmeticsDbContext>();
			builder.Services.AddScoped<IUnitOfWork<CosmeticsDbContext>, UnitOfWork<CosmeticsDbContext>>();
			builder.Services.AddScoped<IUnitOfWork<DbContext>, UnitOfWork<DbContext>>();
			builder.Services.AddScoped<ISystemAccountService, SystemAccountService>();
			builder.Services.AddScoped<ICosmeticInformationService, CosmeticInformationService>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork<CosmeticsDbContext>>();

			builder.Services
			.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = false,
					ValidIssuer = configuration["JWT:Issuer"],
					ValidAudience = configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
				};
			});

			builder.Services.AddSwaggerGen(c =>
			{
				var jwtSecurityScheme = new OpenApiSecurityScheme
				{
					Name = "JWT Authentication",
					Description = "JWT Authentication for Cosmetics Management",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT"
				};

				c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

				var securityRequirement = new OpenApiSecurityRequirement
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
							new string[] {}
						}
					};

				c.AddSecurityRequirement(securityRequirement);
			});

			builder.Services.AddAuthorization(options =>
			{

				options.AddPolicy("AdminOnly",
					policyBuilder => policyBuilder.RequireAssertion(
						context => context.User.HasClaim(claim => claim.Type == "Role") &&
						context.User.FindFirst(claim => claim.Type == "Role").Value == "1"));

				options.AddPolicy("AdminOrStaffOrMember",
					policyBuilder => policyBuilder.RequireAssertion(
						context => context.User.HasClaim(claim => claim.Type == "Role")
						&& (context.User.FindFirst(claim => claim.Type == "Role").Value == "1"
						|| context.User.FindFirst(claim => claim.Type == "Role").Value == "3"
						|| context.User.FindFirst(claim => claim.Type == "Role").Value == "4")));
			});



			var app = builder.Build();

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