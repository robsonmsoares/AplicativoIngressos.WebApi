using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoIngressos.WebApi.Models
{
    public sealed class NovoFilmeInputModel
    {
        [Required]
        [MinLength(10, ErrorMessage = "Tamanho inválido")]
        public string Titulo { get; set; }
        public string Duracao { get; set; }
        public string Sinopse { get; set; }
    }
}
