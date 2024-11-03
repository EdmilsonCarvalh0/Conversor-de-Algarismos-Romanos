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
        private static Dictionary<Algarismo, List<Algarismo>> PosterioresDaSubtracaoValidos { get; }

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

            PosterioresDaSubtracaoValidos = new Dictionary<Algarismo, List<Algarismo>>
            {
                {l, new List<Algarismo> {v,i} },
                {c, new List<Algarismo> {v,i} },
                {d, new List<Algarismo> {l,x,v,i} },
                {m, new List<Algarismo> {l,x,v,i } },
            };
        }

        public void ChecarPosterioresDaSubtracao(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            if (PosterioresDaSubtracaoValidos.ContainsKey(primeiroAlgarismo))
            {
                var algarismosPermitidos = PosterioresDaSubtracaoValidos[primeiroAlgarismo];
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
                    if (algarismosPermitidos.Contains(segundoAlgarismo))
                    {
                        return true;
                    }
                    else
                    {
                        LancarExcecaoDePosterioridadeInvalida();
                    }
                }
                else
                {
                    LancarExcecaoDePosterioridadeInvalida();
                }
            }
            return false;
        }

        // TRANSFERIR EXCEÇÕES PARA O VALIDADOR POIS NECESSITA REINICIAR O _estado
        public void LancarExcecaoDePosterioridadeInvalida()
        {
            throw new NumeralInvalidoException($"O numeral apresenta uma sequência inválida de algarismos.");
        }
    }
}
