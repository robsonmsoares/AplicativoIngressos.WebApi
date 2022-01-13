using AplicacaoIngressos.WebApi.Dominio;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplicacaoIngressos.WebApi.Infraestrutura.EntityConfigurations
{
    public static class EFConversores
    {
        public static readonly ValueConverter<Horario, string> HorarioConverter
            = new ValueConverter<Horario, string>(
            horario => horario.ToString(),
            valorBD => Horario.Criar(valorBD).Value);
    }
}
