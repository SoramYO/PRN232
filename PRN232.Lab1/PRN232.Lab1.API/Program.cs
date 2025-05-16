
using Microsoft.AspNetCore.Authorization;
using PRN232.Lab1.API.Authorization;
using PRN232.Lab1.API.Extensions;
using PRN232.Lab1.API.Middlewares;
using System.Text.Json.Serialization;

namespace PRN232.Lab1.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();
			builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

			// Database and Repository registrations
			builder.Services.AddDatabase();
			builder.Services.AddUnitOfWork();

			// Add Authorization services
			builder.Services.AddAuthentication();
			builder.Services.AddAuthorization();


			// Other service registrations
			builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: "MyDefaultPolicy",
					policy =>
					{
						policy
							.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
							.AllowAnyHeader()
							.AllowAnyMethod()
							.AllowCredentials();
					});
			});

			// Existing service registrations...
			builder.Services.AddSingleton<IAuthorizationHandler, HeaderRequirementHandler>();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddServices(builder.Configuration);
			builder.Services.AddConfigSwagger();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddHttpClient();
			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
			var app = builder.Build();

			app.UseSwagger();
			app.UseSwaggerUI();
			app.UseMiddleware<ExceptionHandlingMiddleware>();
			app.UseCors("MyDefaultPolicy");
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
