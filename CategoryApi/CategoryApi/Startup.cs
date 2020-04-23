using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CategoryApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace CategoryApi
{
    public class Startup
    {
        //static logger
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(Startup)); //Initialize Logger
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AMCDbContext>(options => options
                .UseMySql("Server=localhost; Database=jeopardy;User=root;Password=ssjdnv;",
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
                         Email = "Jordan.sali@canada.ca",
                         Name = "Jordan Sali",
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


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("swagger/CategorySpec/swagger.json",
                "CategoryApi");
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
