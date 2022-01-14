using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CSharpFunctionalExtensions;

namespace AplicacaoIngressos.WebApi.Dominio
{
    public sealed class Ingresso
    {
        public Guid Id { get; private set; }
        public Guid SessaoId { get; private set; }
        public string NomeCliente { get; private set; }
        public int Quantidade { get; private set; }

        private Ingresso() { }

        public Ingresso(Guid id, Guid sessaoId, string nomeCliente, int quantidade)
        {
            Id = id;
            SessaoId = sessaoId;
            NomeCliente = nomeCliente;
            Quantidade = quantidade;
        }

        public static Result<Ingresso> Criar(Guid sessaoId, string nomeCliente, int quantidade)
        {
            return new Ingresso(Guid.NewGuid(), sessaoId, nomeCliente, quantidade);
        }
    }
}
