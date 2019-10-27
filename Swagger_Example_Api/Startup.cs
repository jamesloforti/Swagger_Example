// ******************************************************************************************************************
//  This file is part of Swagger_Example.
//
//  Swagger_Example - simple example to demonstrate swagger UI.
//  Copyright(C) 2019  James LoForti
//  Contact Info: jamesloforti@gmail.com
//
//  Swagger_Example is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Affero General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.
//									     ____.           .____             _____  _______   
//									    |    |           |    |    ____   /  |  | \   _  \  
//									    |    |   ______  |    |   /  _ \ /   |  |_/  /_\  \ 
//									/\__|    |  /_____/  |    |__(  <_> )    ^   /\  \_/   \
//									\________|           |_______ \____/\____   |  \_____  /
//									                             \/          |__|        \/ 
//
// ******************************************************************************************************************
//
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swagger_Example_Api.Interfaces;
using Swagger_Example_Api.Model;
using Swashbuckle.AspNetCore.Swagger;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Swagger_Example_Api
{
	public class Startup
	{
		private IAppSettings _appSettings;

		private IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			_appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
			services.AddSingleton(_appSettings);

			services.AddHttpContextAccessor();
			services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
			services.AddSwaggerGen(c =>
			{
				c.EnableAnnotations();
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = _appSettings.ApplicationName,
					Description = "Simple example to demonstrate swagger.",
					TermsOfService = "GNU General Public License",
					Contact = new Contact()
					{
						Name = "James LoForti",
						Email = "jamesloforti@gmail.com",
						Url = "www.jimmyloforti.com"
					}
				});
			});

			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			services.AddSingleton(h => httpClient);

			Log.Information($"{_appSettings.ApplicationName} has started successfully.");
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
				app.UseHsts();

			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint(_appSettings.SwaggerJsonUrl, _appSettings.ApplicationName);
				c.DocumentTitle = _appSettings.ApplicationName; // swagger ui web page title
				c.RoutePrefix = "swagger"; // swagger ui url path (also see launchSettings under Properties)
			});
			app.UseSerilogRequestLogging(); // must be last method called on app
		}
	}
}
