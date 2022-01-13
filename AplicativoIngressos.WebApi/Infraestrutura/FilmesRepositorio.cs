using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
