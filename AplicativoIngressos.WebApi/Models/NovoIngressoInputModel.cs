using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoIngressos.WebApi.Models
{
    public class NovoIngressoInputModel
    {
        public string SessaoId { get; set; }
        public string NomeCliente { get; set; }
        public int Quantidade { get; set; }
    }
}
