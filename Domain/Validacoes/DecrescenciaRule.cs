using ConversorAlgarismoRomano.Exceptions;
using ConversorAlgarismoRomano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Rules.Validacoes
{
    internal class DecrescenciaRule : IRegraValidacao
    {
        private static Dictionary<Algarismo, List<Algarismo>> Posteriores { get; }

        static DecrescenciaRule()
        {
            Algarismo i = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('i')!)!;
            Algarismo v = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('v')!)!;
            Algarismo x = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('x')!)!;
            Algarismo l = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('l')!)!;
            Algarismo c = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('c')!)!;
            Algarismo d = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('d')!)!;
            Algarismo m = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('m')!)!;

            Posteriores = new Dictionary<Algarismo, List<Algarismo>>
            {
                {i, new List<Algarismo> {i,v,x} },
                {v, new List<Algarismo> {i} },
                {x, new List<Algarismo> {i,v,x,l,c} },
                {l, new List<Algarismo> {i,v,x} },
                {c, new List<Algarismo> {i,v,x,c,l,d,m} },
                {d, new List<Algarismo> {i,v,x,l,c} },
                {m, new List<Algarismo> {i,v,x,l,c,d,m} }
            };
        }

        public bool ChecarDecrescencia(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            return primeiroAlgarismo.ValorCorrespondente > segundoAlgarismo.ValorCorrespondente;
        }

        public void ChecarPosteriores(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            if (Posteriores.ContainsKey(primeiroAlgarismo))
            {
                var algarismosPermitidos = Posteriores[primeiroAlgarismo];
                bool posterioridadeValida = algarismosPermitidos.Contains(segundoAlgarismo);
                if (!posterioridadeValida)
                {
                    // TRANSFERIR EXCEÇÕES PARA O VALIDADOR POIS NECESSITA REINICIAR O _estado
                    throw new NumeralInvalidoException($"DECRESCENCIA O numeral apresenta sequência inválida de algarismo.");
                }
            }
        }
    }
}
