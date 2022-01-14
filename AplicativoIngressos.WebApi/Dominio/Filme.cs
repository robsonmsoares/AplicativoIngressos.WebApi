using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CSharpFunctionalExtensions;

namespace AplicacaoIngressos.WebApi.Dominio
{
    public sealed class Filme
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string Duracao { get; private set; }
        public string Sinopse { get; private set; }

        private Filme() { }

        public Filme(Guid id, string titulo, string duracao, string sinopse)
        {
            Id = id;
            Titulo = titulo;
            Duracao = duracao;
            Sinopse = sinopse;
        }

        public static Result<Filme> Criar(string titulo, string duracao, string sinopse)
        {
            if (string.IsNullOrEmpty(titulo))
                return Result.Failure<Filme>("Título deve ser preenchido");
            return new Filme(Guid.NewGuid(), titulo, duracao, sinopse);
        }
    }
}
