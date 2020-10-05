using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PrimerWebApiM3.Context;
using PrimerWebApiM3.Entities;
using PrimerWebApiM3.Models;
using PrimerWebApiM3.Services;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace PrimerWebApiM3
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
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));


            //Servicios
            services.AddScoped<IAutoresRepository, AutoresRepository>();
       
            //AutoMapper
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper( config =>
            {
                config.CreateMap<Autor, AutorDTO>();
                config.CreateMap<AutorCreacionDTO, Autor>();
                config.CreateMap<Libro, LibroDTO>();
            },
                typeof (Startup)
            );

            //Swagger
            services.AddSwaggerGen(config => config.SwaggerDoc("v1", new OpenApiInfo { Title = "Primer Web API", Version = "v1" }));


        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
