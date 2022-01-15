using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoIngressos.WebApi.Dominio;

namespace AplicacaoIngressos.WebApi.Infraestrutura
{
    public sealed class FilmesRepositorio
    {
        private readonly IngressosDbContext _dbContext;

        public FilmesRepositorio(IngressosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Filme> RecuperarPorId(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                            .Filmes
                            .FirstOrDefaultAsync(filme => filme.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Filme>> RecuperarTodos(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Filmes.Include(filme => filme.Sessoes).ToListAsync(cancellationToken);
        }

        public async Task Inserir(Filme novoFilme, CancellationToken cancellationToken = default)
        {
            await _dbContext.Filmes.AddAsync(novoFilme, cancellationToken);
        }

        public void Atualizar(Filme filme)
        {
        }

        public void Remover(Filme removerFilme)
        {
            _dbContext.Filmes.Remove(removerFilme);
        }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}