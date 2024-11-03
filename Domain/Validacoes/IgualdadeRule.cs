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
            return primeiroAlgarismo.Equals(segundoAlgarismo) ? true : false;
        }

        public bool ChecarRepeticaoInvalida(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            return AlgarismosSemRepeticao.Contains(segundoAlgarismo) ? true : false;
        }
    }
}
