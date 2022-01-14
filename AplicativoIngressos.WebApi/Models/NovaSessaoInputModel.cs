using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoIngressos.WebApi.Models
{
    public sealed class NovaSessaoInputModel
    {
        public string FilmeId { get; set; }
        public string Data { get; set; }
        public string HoraInicial { get; }
        public string HoraFinal { get; }
        public int QuantidadeLugares { get; set; }
        public double Preco { get; set; }
    }
}
