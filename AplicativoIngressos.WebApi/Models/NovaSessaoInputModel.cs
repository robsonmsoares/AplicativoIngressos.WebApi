using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoIngressos.WebApi.Models
{
    public sealed class NovaSessaoInputModel
    {
        [Required]
        public string FilmeId { get; set; }
        [Required]
        public string Data { get; set; }
        [Required]
        public string HoraInicial { get; }
        [Required]
        public string HoraFinal { get; }
        [Required]
        public int QuantidadeLugares { get; set; }
        [Required]
        public double Preco { get; set; }
    }
}
