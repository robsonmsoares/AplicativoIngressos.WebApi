using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CSharpFunctionalExtensions;

namespace AplicacaoIngressos.WebApi.Dominio
{
    public sealed class Sessao
    {
        public Guid Id { get; private set; }
        public Guid FilmeId { get; private set; }
        public DateTime Data { get; private set; }
        public DateTime HorarioInicio { get; private set; }
        public int QuantidadeLugares { get; private set; }
        public double Preco { get; private set; }

        private Sessao() { }

        public Sessao(Guid id, Guid filmeId, DateTime data, DateTime horarioInicio,
                      int quantidadeLugares, double preco)
        {
            Id = id;
            FilmeId = filmeId;
            Data = data;
            HorarioInicio = horarioInicio;
            QuantidadeLugares = quantidadeLugares;
            Preco = preco;
        }

        public static Result<Sessao> Criar(Guid filmeId, DateTime data, DateTime horarioInicio,
            int quantidadeLugares, double preco)
        {
            return new Sessao(Guid.NewGuid(), filmeId, data, horarioInicio, quantidadeLugares, preco);
        }
    }
}
