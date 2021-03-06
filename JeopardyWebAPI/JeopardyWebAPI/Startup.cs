using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using JeopardyWebAPI.Models;
using JeopardyWebAPI.Data;
using JeopardyWebAPI.Data.EFCore;

namespace JeopardyWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Azure requires the following:
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("swagger/CategorySpec/swagger.json",
                "JeopardyApi");
                setupAction.RoutePrefix = ""; //will make swagger availiable at root
            });

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<JeopardyDbContext>(options => options
             .UseMySql(Configuration.GetConnectionString("FeltGameContext"),
                    mysqlOptions =>
                        mysqlOptions.ServerVersion(new ServerVersion(new Version(10, 4, 6), ServerType.MariaDb))));


            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                });

            });

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
            services.AddScoped<IJeopardyRepository, JeopardyRepository>();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new JeopardyMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           );
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

            //services.AddControllers().AddJsonOptions(options => options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);


        }
    }
}
