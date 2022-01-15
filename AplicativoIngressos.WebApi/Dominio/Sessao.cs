using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations;
using AplicacaoIngressos.WebApi.Models;

namespace AplicacaoIngressos.WebApi.Dominio
{
    public sealed class Sessao
    {
        private IList<Ingresso> _ingressos;

        [Required]
        public Guid Id { get; private set; }
        [Required]
        public Guid FilmeId { get; private set; }
        public DateTime DataHora { get; private set; }
        public int QuantidadeLugares { get; private set; }
        public double Preco { get; private set; }
        public IEnumerable<Ingresso> Ingressos => _ingressos;

        private Sessao()
        {
        }

        public Sessao(Guid id, Guid filmeId, DateTime dataHora,
                      int quantidadeLugares, double preco, List<Ingresso> ingressos)
        {
            Id = id;
            FilmeId = filmeId;
            DataHora = dataHora;
            QuantidadeLugares = quantidadeLugares;
            Preco = preco;
            _ingressos = ingressos;
        }

        public static Result<Sessao> Criar(Guid filmeId, DateTime dataHora,
            int quantidadeLugares, double preco)
        {
            return new Sessao(Guid.NewGuid(), filmeId, dataHora, quantidadeLugares, preco, new List<Ingresso>());
        }

        public void Atualizar(AtualizarSessaoInputModel atualizarSessaoInputModel)
        {
            FilmeId = Guid.Parse(atualizarSessaoInputModel.FilmeId);
            DataHora = DateTime.Parse(atualizarSessaoInputModel.DataHora);
            QuantidadeLugares = atualizarSessaoInputModel.QuantidadeLugares;
            Preco = atualizarSessaoInputModel.Preco;
        }
    }
}
