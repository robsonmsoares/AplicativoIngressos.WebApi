namespace AplicacaoIngressos.WebApi.Models
{
    public class AtualizarSessaoInputModel
    {
        public string FilmeId { get; set; }
        public string DataHora { get; set; }
        public int QuantidadeLugares { get; set; }
        public double Preco { get; set; }
    }
}
