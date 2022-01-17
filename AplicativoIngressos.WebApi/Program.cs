using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;


namespace AplicacaoIngressos.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Information("Aplica��o iniciando!");

            try
            {
                CreateHostBuilder(args).Build().Run();

                Log.Information("Aplica��o encerrada com sucesso!");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Erro iniciando a aplica��o!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var configSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configSettings)
                .CreateLogger();


            return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.AddConfiguration(configSettings);
            })
            .ConfigureLogging(logging =>
            {
                logging.AddSerilog();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}
