using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using CategoriesAPI.Data.EFCore;

namespace CategoriesAPI
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
            //
            //TODO: Mapping
            //

            services.AddControllers();                       
            services.AddDbContext<AMCDbContext>(options => options
             .UseMySql(Configuration.GetConnectionString("FeltGameContext"),
                    mysqlOptions =>
                        mysqlOptions.ServerVersion(new ServerVersion(new Version(10, 4, 6), ServerType.MariaDb))));

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("CategorySpec",
               new Microsoft.OpenApi.Models.OpenApiInfo()
               {
                   Title = "Category API",
                   Version = "beta",
                   Description = "Through this API you can access authors and books.",
                   Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                   {
                       Email = "Daniel.Spagnuolo@canada.ca",
                       Name = "Daniel Spagnuolo",
                       Url = new Uri("https://twitter.com/CNSC_CCSN")
                   },
                   License = new Microsoft.OpenApi.Models.OpenApiLicense()
                   {
                       Name = "MIT License",
                       Url = new Uri("https://opensource.org/licenses/MIT")
                   }
               });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);

            });
            services.AddScoped<EfCoreCategoryRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("swagger/CategorySpec/swagger.json",
                "CategoriesApi");
                setupAction.RoutePrefix = ""; //will make swagger availiable at root
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
