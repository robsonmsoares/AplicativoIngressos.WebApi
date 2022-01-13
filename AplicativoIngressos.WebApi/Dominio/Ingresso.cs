using System;

namespace AplicacaoIngressos.WebApi.Dominio
{
    public sealed class Ingresso
    {
        public Guid Id { get; private set; }
        public Guid SessaoId { get; private set; }
        public string NomeCliente { get; private set; }
        public int Quantidade { get; private set; }

        public Ingresso(Guid id, Guid sessaoId, string nomeCliente, int quantidade)
        {
            Id = id;
            SessaoId = sessaoId;
            NomeCliente = nomeCliente;
            Quantidade = quantidade;
        }
    }
}
