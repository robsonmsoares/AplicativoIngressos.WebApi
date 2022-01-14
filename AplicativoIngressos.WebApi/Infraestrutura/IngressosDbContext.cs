using Microsoft.EntityFrameworkCore;
using AplicacaoIngressos.WebApi.Dominio;
using AplicacaoIngressos.WebApi.Infraestrutura.EntityConfigurations;

namespace AplicacaoIngressos.WebApi.Infraestrutura
{
    public class IngressosDbContext : DbContext
    {
        public IngressosDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
        public DbSet<Ingresso> Ingressos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration();
            //modelBuilder.ApplyConfiguration();
            //modelBuilder.ApplyConfiguration();
        }
    }
}
