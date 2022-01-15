using System.ComponentModel.DataAnnotations;

namespace AplicacaoIngressos.WebApi.Models
{
    public class AtualizarFilmeInputModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Título do filme deve ter no mínimo 5 caracteres")]
        public string Titulo { get; set; }
        public string Duracao { get; set; }
        public string Sinopse { get; set; }
    }
}
