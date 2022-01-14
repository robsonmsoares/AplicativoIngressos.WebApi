using System;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoIngressos.WebApi.Dominio;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoIngressos.WebApi.Infraestrutura
{
    public sealed class FilmesRepositorio
    {
        private readonly IngressosDbContext _dbContext;

        public FilmesRepositorio(IngressosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InserirAsync(Filme novoFilme, CancellationToken cancellationToken = default)
        {
            await _dbContext.Filmes.AddAsync(novoFilme, cancellationToken);
        }

        public async Task<Filme> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                            .Filmes
                            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
