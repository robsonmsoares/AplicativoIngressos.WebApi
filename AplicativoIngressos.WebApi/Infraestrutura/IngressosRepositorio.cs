using System;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoIngressos.WebApi.Dominio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AplicacaoIngressos.WebApi.Infraestrutura
{
    public sealed class IngressosRepositorio
    {
        private readonly IngressosDbContext _dbContext;

        public IngressosRepositorio(IngressosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Ingresso> RecuperarPorId(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                            .Ingressos
                            .FirstOrDefaultAsync(ingresso => ingresso.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Ingresso>> RecuperarTodos(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Ingressos.ToListAsync(cancellationToken);
        }

        public async Task Inserir(Ingresso novoIngresso, CancellationToken cancellationToken = default)
        {
            await _dbContext.Ingressos.AddAsync(novoIngresso, cancellationToken);
        }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}