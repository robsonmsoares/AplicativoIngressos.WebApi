using System;

namespace AplicacaoIngressos.WebApi.Dominio
{
    public sealed class Sessao
    {
        public Guid Id { get; private set; }
        public Guid FilmeId { get; private set; }
        public DateTime Data { get; private set; }
        public Horario HoraInicial { get; }
        public Horario HoraFinal { get; }
        public int QuantidadeLugares { get; private set; }
        public double Preco { get; private set; }

        private Sessao() { }

        public Sessao(Guid id, Guid filmeId, DateTime data, Horario horaInicial,
                      Horario horaFinal, int quantidadeLugares, double preco)
        {
            Id = id;
            FilmeId = filmeId;
            Data = data;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
            QuantidadeLugares = quantidadeLugares;
            Preco = preco;
        }
    }
}
