using ConversorAlgarismoRomano.Exceptions;
using ConversorAlgarismoRomano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Rules.Validacoes
{
    internal class IgualdadeRule : IRegraValidacao
    {
        private static List<Algarismo> AlgarismosSemRepeticao { get; }

        static IgualdadeRule()
        {
            Algarismo v = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('v')!)!;
            Algarismo l = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('l')!)!;
            Algarismo d = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('d')!)!;

            AlgarismosSemRepeticao = new List<Algarismo> { v, l, d };
        }

        public bool ChecarIgualdade(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            bool igual = (primeiroAlgarismo.Equals(segundoAlgarismo));
            if (igual)
            {
                ChecarRepeticaoInvalida(primeiroAlgarismo, segundoAlgarismo);
                return true;
            }
            return false;
        }

        private void ChecarRepeticaoInvalida(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            if (AlgarismosSemRepeticao.Contains(segundoAlgarismo))
            {
                throw new NumeralInvalidoException($"O numeral apresenta sequência inválida de algarismo.");
            }
        }
    }
}
