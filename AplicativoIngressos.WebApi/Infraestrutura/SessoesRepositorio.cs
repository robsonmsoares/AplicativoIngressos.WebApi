using System;
using System.Collections.Generic;
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

        public async Task<Sessao> RecuperarPorId(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                            .Sessoes
                            .Include(c => c.Ingressos)
                            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        }

        public async Task<IEnumerable<Sessao>> RecuperarTodas(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Sessoes.Include(c => c.Ingressos).ToListAsync(cancellationToken);
        }

        public async Task Inserir(Sessao novaSessao, CancellationToken cancellationToken = default)
        {
            await _dbContext.Sessoes.AddAsync(novaSessao, cancellationToken);
        }

        public void Atualizar(Sessao sessao)
        {
        }

        public void Remover(Sessao removerSessao)
        {
            _dbContext.Sessoes.Remove(removerSessao);
        }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}