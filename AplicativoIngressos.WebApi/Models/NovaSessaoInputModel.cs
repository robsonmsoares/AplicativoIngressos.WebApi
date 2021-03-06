using System.ComponentModel.DataAnnotations;

namespace AplicacaoIngressos.WebApi.Models
{
    public sealed class NovaSessaoInputModel
    {
        [Required]
        public string FilmeId { get; set; }
        [Required]
        public string DataHora { get; set; }
        public int QuantidadeLugares { get; set; }
        public double Preco { get; set; }
    }
}
