using CSharpFunctionalExtensions;
using System;

namespace AplicacaoIngressos.WebApi.Dominio
{
    public struct Horario
    {
        public int Hora { get; private set; }
        public int Minuto { get; private set; }

        public Horario(int hora, int minuto)
        {
            Hora = hora;
            Minuto = minuto;
        }

        public override string ToString()
        {
            return $"{Hora:D2}:{Minuto:D2}";
        }

        public static Result<Horario> Criar(string horario)
        {
            var time = horario.Split(":");
            if (time.Length != 2)
                return Result.Failure<Horario>("Hora especificada no banco em formato inválida");

            return new Horario(Int32.Parse(time[0]), Int32.Parse(time[1]));
        }
    }
}
