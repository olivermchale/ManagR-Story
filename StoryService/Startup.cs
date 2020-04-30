using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoryService.Data;
using StoryService.Repository;
using StoryService.Repository.Interfaces;
using StoryService.Services;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace StoryService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)

        {
            Configuration = configuration;
            Environment = environment;
        }
        public IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add cors policy to prevent CORS attacks.
            services.AddCors(options =>
            {
                options.AddPolicy("ManagRAppServices",
                builder =>
                {
                    builder.WithOrigins("https://localhost:4200",
                                        "http://localhost:4200")
                                        .AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader();
                });
            });

            // Add authentication to ManagRs internal auth service, URL changing automatically depending on environment
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var accountUri = Configuration.GetValue<Uri>("AccountsUrl");
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", config =>
            {
                config.Authority = accountUri.ToString();
                config.RequireHttpsMetadata = false;
                config.Audience = "ManagR";
            });

            // Add policies and their required claims
            services.AddAuthorization(options =>
            {
                options.AddPolicy("leader", builder =>
                {
                    builder.RequireClaim("role", "analyst", "leader");
                });
                options.AddPolicy("user", builder =>
                {
                    builder.RequireClaim("role", "user", "spectator");
                });
                options.AddPolicy("spectator", builder =>
                {
                    builder.RequireClaim("role", "spectator");
                });
            });

            // Add db
            services.AddDbContext<StoryServiceDb>(options => options.UseSqlServer(
             Configuration.GetConnectionString("PurchaseOrders")));

            // provide scoped repositories for dependency injection
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<IAgileItemRepository, AgileItemRepository>();
            services.AddScoped<IChartsRepository, ChartsRepository>();

            // Add background service

            //if(!Environment.IsDevelopment())
            //{
            //    services.AddHostedService<BackgroundChartService>();
            //}
            

            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("ManagRAppServices");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // default controller notation
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
