using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Timelogger.Entities;
using Timelogger.Api.Services.Project;
using Timelogger.Api.Services.TimeRegistration;
using Timelogger.Api.Services.Company;
using Timelogger.Api.Services.TimeRegistration.cs;
using Timelogger.Persistence.Contracts;
using Timelogger.Persistence.TimeRegistration;
using Timelogger.Persistence.Project;
using Timelogger.Persistence.Company;
using System.Collections.Generic;

namespace Timelogger.Api
{
	public class Startup
	{
		private readonly IWebHostEnvironment _environment;
		public IConfigurationRoot Configuration { get; }

		public Startup(IWebHostEnvironment env)
		{
			_environment = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("e-conomic interview"));
			services.AddLogging(builder =>
			{
				builder.AddConsole();
				builder.AddDebug();
			});

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "TimeLogger API",
					Version = "v1",
					Description = "Visma TimeLogger Api demo",
				});
				options.CustomSchemaIds(x => x.FullName);
			});


			/*Services *****************************************************************************************************/
			services.AddTransient(typeof(IProjectService), typeof(ProjectService));
			services.AddTransient(typeof(ITimeRegistrationService), typeof(TimeRegistrationService));
			services.AddTransient(typeof(ICompanyService), typeof(CompanyService));

			/*Persistance *****************************************************************************************************/
			services.AddTransient(typeof(IProjectRepository), typeof(ProjectRepository));
			services.AddTransient(typeof(ICompanyRepository), typeof(CompanyRepository));
			services.AddTransient(typeof(ITimeRegistrationRepository), typeof(TimeRegistrationRepository));

			services.AddMvc(options => options.EnableEndpointRouting = false);

			if (_environment.IsDevelopment())
			{
				services.AddCors();
			}
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseCors(builder => builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed(origin => true)
					.AllowCredentials());
			}

			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Visma Timelogger API demo"));


			var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
			using (var scope = serviceScopeFactory.CreateScope())
			{
				SeedDatabase(scope);
			}
		}

		private static void SeedDatabase(IServiceScope scope)
		{
			var context = scope.ServiceProvider.GetService<ApiContext>();
			var company = new Entities.Company()
			{
				Address = "Test Adress nr 10, 4100 Ringsted",
				Id = System.Guid.NewGuid(),
				Name = "Test Virksomhed",
			};

			var project = new Entities.Project()
			{
				CompanyId = company.Id,
				Deadline = System.DateTime.Now.AddDays(7),
				Start = System.DateTime.Now,
				HourlyRate = 100,
				Id = System.Guid.NewGuid(),
				Name = "Test project"
			};

			var listRegistrations = new List<TimeRegistration>();

			for(int i = 0; i<10; i++)
            {
				listRegistrations.Add(new TimeRegistration()
				{
					Date = System.DateTime.Now.AddDays(i),
					Id = System.Guid.NewGuid(),
					Minutes = 60 * i,
					ProjectId = project.Id
				});
            }


			context.Companies.Add(company);
			context.Projects.Add(project);
			context.TimeRegistrations.AddRange(listRegistrations);
			context.SaveChanges();
		}
	}
}