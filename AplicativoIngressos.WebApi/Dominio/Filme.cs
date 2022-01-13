using System;

namespace AplicacaoIngressos.WebApi.Dominio
{
    public sealed class Filme
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string Duracao { get; private set; }
        public string Sinopse { get; private set; }

        private Filme() { }

        private Filme(Guid id, string titulo, string duracao, string sinopse)
        {
            Id = id;
            Titulo = titulo;
            Duracao = duracao;
            Sinopse = sinopse;
        }
    }
}
