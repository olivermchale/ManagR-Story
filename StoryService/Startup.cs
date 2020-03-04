using System;
using System.Collections.Generic;
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
            services.AddCors(options =>
            {
                options.AddPolicy("ManagRAppServices",
                builder =>
                {
                    builder.WithOrigins("https://localhost:4200",
                                        "http://localhost:4200")
                                        .AllowAnyOrigin()
                                        .AllowAnyMethod();
                });
            });

            services.AddDbContext<StoryServiceDb>(options => options.UseSqlServer(
             Configuration.GetConnectionString("PurchaseOrders")));

            services.AddScoped<IBoardRepository, BoardRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("ManagRAppServices");

            app.UseEndpoints(endpoints =>
            {
                // default controller notation
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
