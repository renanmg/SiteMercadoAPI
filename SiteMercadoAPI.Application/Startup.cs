using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using SiteMercadoAPI.Domain.Interfaces;
using SiteMercadoAPI.Infra.Repository.Context;
using SiteMercadoAPI.Infra.Repository.Repositories;

namespace SiteMercadoAPI.Application
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddResponseCompression();
            services.AddScoped<DataContext, DataContext>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddCors();

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
               {
                   Version = "v1",
                   Title = "Cadastro de produtos",
                   Description = "Available Web APIs"
               });
           });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
           );

            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Cadastro de produtos API");
            });

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
                RequestPath = new PathString("/Images")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
