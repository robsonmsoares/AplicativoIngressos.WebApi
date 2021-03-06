using System.ComponentModel.DataAnnotations;

namespace AplicacaoIngressos.WebApi.Models
{
    public sealed class NovoFilmeInputModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Nome do filme deve ter no mínimo 5 caracteres")]
        public string Titulo { get; set; }
        [Required]
        public string Duracao { get; set; }
        public string Sinopse { get; set; }
    }
}
