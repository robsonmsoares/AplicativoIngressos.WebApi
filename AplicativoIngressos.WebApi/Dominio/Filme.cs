using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using AplicacaoIngressos.WebApi.Models;

namespace AplicacaoIngressos.WebApi.Dominio
{
    public sealed class Filme
    {
        private IList<Sessao> _sessoes;

        [Key]
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string Duracao { get; private set; }
        public string Sinopse { get; private set; }
        public IEnumerable<Sessao> Sessoes => _sessoes;

        private Filme() 
        { 
        }

        public Filme(Guid id, string titulo, string duracao, string sinopse, List<Sessao> sessoes)
        {
            Id = id;
            Titulo = titulo;
            Duracao = duracao;
            Sinopse = sinopse;
            _sessoes = sessoes;
        }

        public static Result<Filme> Criar(NovoFilmeInputModel novoFilmeInputModel)
        {
            var novoFilme = new Filme(Guid.NewGuid(), novoFilmeInputModel.Titulo, novoFilmeInputModel.Duracao, 
                novoFilmeInputModel.Sinopse, new List<Sessao>());

            return novoFilme;
        }

        public void Atualizar(AtualizarFilmeInputModel atualizarFilmeinputModel)
        {
            Titulo = atualizarFilmeinputModel.Titulo;
            Duracao = atualizarFilmeinputModel.Duracao;
            Sinopse = atualizarFilmeinputModel.Sinopse;
        }
    }
}
