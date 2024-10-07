using ConversorAlgarismoRomano.Exceptions;
using ConversorAlgarismoRomano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Rules.Validacoes
{
    internal class SubtracaoRule : IRegraValidacao
    {
        private static Dictionary<Algarismo, List<Algarismo>> Subtracoes { get; }
        private static Dictionary<Algarismo, List<Algarismo>> PosterioresDaSubtracao { get; }

        static SubtracaoRule()
        {
            Algarismo i = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('i')!)!;
            Algarismo v = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('v')!)!;
            Algarismo x = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('x')!)!;
            Algarismo l = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('l')!)!;
            Algarismo c = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('c')!)!;
            Algarismo d = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('d')!)!;
            Algarismo m = Algarismo.GetAlgarismo(Simbolo.GetSimbolo('m')!)!;

            Subtracoes = new Dictionary<Algarismo, List<Algarismo>>
            {
                {i, new List<Algarismo> {v,x} },
                {x, new List<Algarismo> {l,c} },
                {c, new List<Algarismo> {d,m} },
            };

            PosterioresDaSubtracao = new Dictionary<Algarismo, List<Algarismo>>
            {
                {l, new List<Algarismo> {v,i} },
                {c, new List<Algarismo> {v,i} },
                {d, new List<Algarismo> {l,x,v,i} },
                {m, new List<Algarismo> {l,x,v,i } },
            };
        }

        public void ChecarPosterioresDaSubtracao(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            if (PosterioresDaSubtracao.ContainsKey(primeiroAlgarismo))
            {
                var algarismosPermitidos = PosterioresDaSubtracao[primeiroAlgarismo];
                bool posterioridadeValida = algarismosPermitidos.Contains(segundoAlgarismo);
                if (!posterioridadeValida)
                {
                    throw new NumeralInvalidoException($"O numeral apresenta sequência inválida após subtração.");
                }
            }
        }

        public bool ChecarSubtracao(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            bool primeiroMenor = (primeiroAlgarismo.ValorCorrespondente < segundoAlgarismo.ValorCorrespondente);
            bool pertenceAUmaCombinacao = (Subtracoes.ContainsKey(primeiroAlgarismo));

            if (primeiroMenor)
            {
                if (pertenceAUmaCombinacao)
                {
                    var algarismosPermitidos = Subtracoes[primeiroAlgarismo];
                    bool combinacaoPermitida = (algarismosPermitidos.Contains(segundoAlgarismo));
                    if (combinacaoPermitida)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
