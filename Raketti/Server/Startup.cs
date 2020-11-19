using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raketti.Server.Data;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;

namespace Raketti.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton(new SqlConfiguration(Configuration.GetConnectionString("MSSQL")));
			services.AddControllersWithViews();
			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.Use(async (context, next) =>
			{
				double hstsExpire = TimeSpan.FromDays(30).TotalSeconds;
				context.Response.Headers.Add("X-Frame-Options", "DENY");
				context.Response.Headers.Add("Content-Security-Policy", "" +
				"default-src 'self';" +
				"img-src data: https:;" +
				"object-src 'none';" +
				"script-src 'self' 'sha256-9mThMC8NT3dPbcxJOtXiiwevtWTAPorqkXGKqI388cI=' 'sha256-v8v3RKRPmN4odZ1CWM5gw80QKPCCWMcpNeOmimNL2AA=' 'sha256-Nf/DChZ0c94B4rwuAMUxo8txJpPuJsZdlwmpQgdolC0=' 'unsafe-eval';" +
				"style-src 'self' 'unsafe-inline';" +
				"upgrade-insecure-requests;");
				context.Response.Headers.Add("Strict-Transport-Security", $"max-age={hstsExpire};includeSubDomains;preload");
				await next();
			});

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();
			app.UseCookiePolicy(new CookiePolicyOptions {
				HttpOnly = HttpOnlyPolicy.Always,
				Secure = CookieSecurePolicy.Always,
				MinimumSameSitePolicy = SameSiteMode.Strict
			});
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
