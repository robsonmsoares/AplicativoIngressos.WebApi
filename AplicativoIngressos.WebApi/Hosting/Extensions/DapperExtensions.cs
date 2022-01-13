using AplicacaoIngressos.WebApi.Infraestrutura.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Dapper;

namespace AplicacaoIngressos.WebApi.Hosting.Extensions
{
    public static class DapperExtensions
    {
        public static IServiceCollection AddDapper(this IServiceCollection serviceCollection)
        {
            SqlMapper.AddTypeHandler(new HorarioTypeHandler());
            return serviceCollection;
        }
    }
}
