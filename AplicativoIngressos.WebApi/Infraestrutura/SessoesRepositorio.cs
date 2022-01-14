using System;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoIngressos.WebApi.Dominio;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoIngressos.WebApi.Infraestrutura
{
    public sealed class SessoesRepositorio
    {
        private readonly IngressosDbContext _dbContext;

        public SessoesRepositorio(IngressosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InserirAsync(Sessao novaSessao, CancellationToken cancellationToken = default)
        {
            await _dbContext.Sessoes.AddAsync(novaSessao, cancellationToken);
        }

        public async Task<Sessao> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                            .Sessoes
                            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
