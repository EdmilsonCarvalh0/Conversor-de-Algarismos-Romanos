using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConversorAlgarismoRomano.Models;

namespace ConversorAlgarismoRomano.Services
{
    public class ConversorService
    {
        public static Numeral NumeralValido { get; private set; } = Numeral.GetNumeral();
        public int NumeroConvertido { get; private set; } = 0;
        private static ValidadorNumeralService _validadorService {  get; set; }
        private static bool ConversaoEmAndamento { get; set; } = true;

        static ConversorService()
        {
            _validadorService = new ValidadorNumeralService();
        }

        public int Converter(string inputNumeralUsuario)
        {
            NumeralValido = _validadorService.ValidarNumeral(inputNumeralUsuario);

            //AplicarAlgoritmo();

            return NumeroConvertido;
        }

        private void AplicarAlgoritmo()
        {
            List<Algarismo> algarismos = new List<Algarismo>(NumeralValido.AlgarismosDoNumeral);

            int repetido = 0;

            int i = 0;
            int j = 1;
            int valorAnterior = 0;
            int valorAtual = 0;
            int proximoValor = 0;

            int tamanhoDaLista = algarismos.Count;

            while (ConversaoEmAndamento)
            {
                //         <----------- CRIAR MÉTODO ----------->
                bool finalizouNumeral = VerificarFinalizacaoDaLista(j, tamanhoDaLista);
                if (finalizouNumeral)
                {
                    valorAtual = algarismos[i].ValorCorrespondente;
                    proximoValor = algarismos[j].ValorCorrespondente;

                    if (tamanhoDaLista > 1)
                    {
                        valorAnterior = algarismos[i - 1].ValorCorrespondente;
                        CorrigirFinalDeListaComTamanhoImpar(tamanhoDaLista, valorAtual, valorAnterior);
                        CorrigirFinalDeListaComTamanhoPar(tamanhoDaLista, valorAtual, proximoValor);
                        break;
                    }

                    // CORRIGE QUANDO FINAL DA LISTA TEM SUBTRAÇÃO -> MMMDCIV
                    //bool finalComSubtracao = VerificarSubtracao(valorAnterior, valorAtual);
                    //if (finalComSubtracao)
                    //{
                    //    CorrigirFinalComSubtracao(valorAtual, valorAnterior);
                    //    break;
                    //}

                    //bool finalEstaIgual = VerificarIgualdade(valorAnterior, valorAtual);
                    //if (finalEstaIgual)
                    //{
                    //    ContabilizarValor(valorAtual);
                    //    break;
                    //}
                }

                valorAtual = algarismos[i].ValorCorrespondente;
                proximoValor = algarismos[j].ValorCorrespondente;

                if (i >= 2)
                {
                    valorAnterior = algarismos[i - 1].ValorCorrespondente;
                    if (valorAtual == valorAnterior)
                    {
                        ContabilizarValor(valorAtual);
                        continue;
                    }
                }

                //         <----------- CRIAR MÉTODO ----------->
                bool estaIgual = VerificarIgualdade(valorAtual, proximoValor);
                if (estaIgual)
                {
                    repetido++;
                    ContabilizarDoisValores(valorAtual, proximoValor);
                    i += 2;
                    j += 2;
                    continue;
                }

                //         <----------- CRIAR MÉTODO ----------->
                bool estaDecresccente = VerificarDecrescencia(valorAtual, proximoValor);
                if (estaDecresccente)
                {
                    // CASO FOR REPETIDO -> MM'MDCVI
                    bool repetiuUmaVez = repetido == 1;
                    if (repetiuUmaVez)
                    {
                        valorAnterior = algarismos[i - 1].ValorCorrespondente;
                        estaIgual = VerificarIgualdade(valorAtual, valorAnterior);
                        if (estaIgual)
                        {
                            ContabilizarValor(valorAtual);
                            i++;
                            j++;
                            continue;
                        }
                    }
                    ContabilizarDoisValores(valorAtual, proximoValor);
                    i += 2;
                    j += 2;
                    continue;
                }

                //         <----------- CRIAR MÉTODO ----------->
                bool temSubtracao = VerificarSubtracao(valorAtual, proximoValor);
                if (temSubtracao)
                {
                    ContabilizarValoresDaSubtracao(valorAtual, proximoValor);
                    i += 2;
                    j += 2;
                    continue;
                }
            }
        }


        private bool VerificarFinalizacaoDaLista(int segundoPonteiro, int tamanhoDaLista)
        {
            return segundoPonteiro == tamanhoDaLista || segundoPonteiro == tamanhoDaLista-1;
        }

        private void CorrigirFinalComSubtracao(int valorAtual, int valorAnterior)
        {
            NumeroConvertido = NumeroConvertido - (valorAtual - valorAnterior) + valorAtual - valorAnterior;
        }

        private void CorrigirFinalDeListaComTamanhoImpar(int tamanhoDoNumeral, int valorAtual, int valorAnterior)
        {
            // CORRIGE FINAL DA LISTA -> DCCVI
            if (tamanhoDoNumeral % 2 != 0)
            {
                bool finalDecrescente = VerificarDecrescencia(valorAnterior, valorAtual);
                if (valorAtual < valorAnterior)
                {
                    ContabilizarValor(valorAtual);
                    ConversaoEmAndamento = false;
                }
            }
        }

        private void CorrigirFinalDeListaComTamanhoPar(int tamanhoDoNumeral, int valorAtual, int proximoValor)
        {
            // CORRIGE FINAL DA LISTA -> DCVI
            if (tamanhoDoNumeral % 2 == 0)
            {
                bool finalDecrescente = VerificarDecrescencia(valorAtual, proximoValor);
                if (finalDecrescente)
                {
                    ContabilizarDoisValores(valorAtual, proximoValor);
                    ConversaoEmAndamento = false;
                }

                bool finalIgual = VerificarIgualdade(valorAtual, proximoValor);
                if (finalIgual)
                {
                    ContabilizarDoisValores(valorAtual, proximoValor);
                    ConversaoEmAndamento = false;
                }
            }
        }

        private bool VerificarIgualdade(int valorAtual, int proximoValor)
        {
            return valorAtual == proximoValor;
        }

        private bool VerificarDecrescencia(int primeiroValor, int segundoValor)
        {
            return primeiroValor > segundoValor;
        }

        private bool VerificarSubtracao(int primeiroValor, int segundoValor)
        {
            return primeiroValor < segundoValor;
        }

        private void ContabilizarValoresDaSubtracao(int valorAtual, int proximoValor)
        {
            NumeroConvertido += proximoValor - valorAtual;
        }

        private void ContabilizarDoisValores(int primeiroValor, int segundoValor)
        {
            NumeroConvertido += primeiroValor + segundoValor;
        }

        private void ContabilizarValor(int valorUnico)
        {
            NumeroConvertido += valorUnico;
        }
    }
}
