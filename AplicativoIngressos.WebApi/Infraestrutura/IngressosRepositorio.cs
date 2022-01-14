using System;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoIngressos.WebApi.Dominio;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoIngressos.WebApi.Infraestrutura
{
    public sealed class IngressosRepositorio
    {
        private readonly IngressosDbContext _dbContext;

        public IngressosRepositorio(IngressosDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InserirAsync(Ingresso novoIngresso, CancellationToken cancellationToken = default)
        {
            await _dbContext.Ingressos.AddAsync(novoIngresso, cancellationToken);
        }

        public async Task<Ingresso> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                            .Ingressos
                            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
