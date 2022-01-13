using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoIngressos.WebApi.Models
{
    public class NovoIngressoInputModel
    {
        [Required]
        public string SessaoId { get; set; }
        [Required]
        public string NomeCliente { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}
