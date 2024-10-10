using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConversorAlgarismoRomano.Interfaces;
using ConversorAlgarismoRomano.Models;

namespace ConversorAlgarismoRomano.Services
{
    public class ConversorService
    {
        private readonly IGerenciadorDeValidacoes _gerenciadorDeValidacoes;
        public Numeral Numeral { get; private set; } = new Numeral();
        public int ValorConvertido { get; private set; } = 0;

        public ConversorService()
        {
            _gerenciadorDeValidacoes = new GerenciadorDeValidacoes();
        }

        public void PrepararParaConversao(string input)
        {
            Numeral = _gerenciadorDeValidacoes.IniciarValidacao(input);
        }

        public int Converter()
        {
            if (Numeral.Valido)
            {
                Calcular(Numeral);
            }

            return ValorConvertido;
        }

        private void Calcular(Numeral numeral)
        {
            int tamanhoDoNumeral = numeral.AlgarismosDoNumeral.Count;
            List<Algarismo> algarismos = numeral.AlgarismosDoNumeral;

            for (int i = 0; i < tamanhoDoNumeral; i++)
            {
                if (i > 0 && VerificarSubtracao(algarismos[i].ValorCorrespondente, algarismos[i - 1].ValorCorrespondente))
                {
                    CorrigirValorComSubtracao(algarismos[i].ValorCorrespondente, algarismos[i - 1].ValorCorrespondente);
                }
                else
                {
                    ContabilizarValor(algarismos[i].ValorCorrespondente);
                }
            }
        }

        private bool VerificarSubtracao(int primeiroValor, int segundoValor)
        {
            return primeiroValor > segundoValor;
        }

        private void CorrigirValorComSubtracao(int primeiroValor, int segundoValor)
        {
            ValorConvertido += primeiroValor - 2 * segundoValor;
        }

        private void ContabilizarValor(int valor)
        {
            ValorConvertido += valor;
        }
    }
}