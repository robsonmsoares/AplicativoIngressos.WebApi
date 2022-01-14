using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using AplicacaoIngressos.WebApi.Hosting.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AplicacaoIngressos.WebApi.Infraestrutura;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoIngressos.WebApi
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

            services.AddControllers();
            services.AddScoped<FilmesRepositorio>();
            services.AddScoped<SessoesRepositorio>();
            services.AddScoped<IngressosRepositorio>();
            services.AddDapper();
            services.AddDbContext<IngressosDbContext>(
                o =>
                {
                    o.UseSqlServer("name=ConnectionStrings:Ingressos");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
