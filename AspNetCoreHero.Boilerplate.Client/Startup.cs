using AspNetCoreHero.Boilerplate.Client.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           

            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookie";
                options.DefaultChallengeScheme = "oidc";
            })
        .AddCookie("cookie")
        .AddOpenIdConnect("oidc", options =>
        {
            options.Authority = Configuration["ClientSettings:AuthorityUrl"];
            options.ClientId = Configuration["ClientSettings:ClientId"];
            options.ClientSecret = Configuration["ClientSettings:ClientSecret"];

            options.ResponseType = Configuration["ClientSettings:ResponseType"];
            options.UsePkce = bool.Parse(Configuration["ClientSettings:UsePkce"]);
            options.ResponseMode = Configuration["ClientSettings:ResponseMode"]; ;

            options.Scope.Add(Configuration["ClientSettings:Scopes:0"]);
            options.SaveTokens = true;

        });

            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));

            services.AddSingleton<ITokenService, TokenService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
