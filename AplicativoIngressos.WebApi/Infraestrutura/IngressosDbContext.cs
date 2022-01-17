using Microsoft.EntityFrameworkCore;
using AplicacaoIngressos.WebApi.Dominio;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                foreach (var item in ChangeTracker.Entries())
                {
                    if (item.State is EntityState.Modified or EntityState.Added
                        && item.Properties.Any(c => c.Metadata.Name == "Atualizado"))
                        item.Property("Atualizado").CurrentValue = DateTime.Now;

                    if (item.State == EntityState.Added
                        && item.Properties.Any(c => c.Metadata.Name == "Criado"))
                        item.Property("Criado").CurrentValue = DateTime.Now;
                }

                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
