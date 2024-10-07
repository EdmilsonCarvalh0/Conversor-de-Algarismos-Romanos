using ConversorAlgarismoRomano.Exceptions;
using ConversorAlgarismoRomano.Models;
using ConversorAlgarismoRomano.Rules.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Rules
{
    internal class GerenciadorDeValidacoes
    {
        private static readonly SubtracaoRule _regraSubtracao;
        private static readonly DecrescenciaRule _regraDecrescencia;
        private static readonly IgualdadeRule _regraIgualdade;
        private static EstadoValidacao _estado { get; set; } = new EstadoValidacao();
        private static List<Algarismo> Algarismos { get; set; } = new List<Algarismo>();

        static GerenciadorDeValidacoes()
        {
            _regraSubtracao = new SubtracaoRule();
            _regraDecrescencia = new DecrescenciaRule();
            _regraIgualdade = new IgualdadeRule();
        }

        internal static void Validar(List<Algarismo> algarismosAVerificar)
        {
            Algarismos = algarismosAVerificar;

            if (Algarismos.Count == 1) return;

            Algarismo algarismoAtual = Algarismos[_estado.PonteiroAtual];

            if (_estado.PonteiroSeguinte > algarismosAVerificar.Count-1) return;

            Algarismo proximoAlgarismo = Algarismos[_estado.PonteiroSeguinte];

            AplicarRegras(algarismoAtual, proximoAlgarismo);

            Validar(Algarismos);
        }

        private static void AplicarRegras(Algarismo algarismoAtual, Algarismo proximoAlgarismo)
        {
            AplicarValidacaoSubtracao(algarismoAtual, proximoAlgarismo);
            AplicarValidacaoDecrescencia(algarismoAtual, proximoAlgarismo);
            AplicarValidacaoIgualdade(algarismoAtual, proximoAlgarismo);

            _estado.ExisteSubtracao = _regraSubtracao.ChecarSubtracao(algarismoAtual, proximoAlgarismo);
            _estado.EstaDecrescente = _regraDecrescencia.ChecarDecrescencia(algarismoAtual, proximoAlgarismo);
            _estado.EstaIgual = _regraIgualdade.ChecarIgualdade(algarismoAtual, proximoAlgarismo);
        }

        private static void AplicarValidacaoDecrescencia(Algarismo algarismoAtual, Algarismo proximoAlgarismo)
        {
            if (_estado.EstaDecrescente)
            {
                _regraDecrescencia.ChecarPosteriores(algarismoAtual, proximoAlgarismo);
                _estado.PonteiroAtual++;
                _estado.PonteiroSeguinte++;
            }
        }

        private static void AplicarValidacaoIgualdade(Algarismo algarismoAtual, Algarismo proximoAlgarismo)
        {
            if (_estado.EstaIgual)
            {
                ChecarBlocoRepeticao();
                VerificarPosterioridadeDoBlocoRepeticao(algarismoAtual, proximoAlgarismo);

                AvaliarRepeticaoUnicaComDecrescencia(algarismoAtual, proximoAlgarismo);
                AvaliarRepeticaoDuplaComDecrescencia(algarismoAtual, proximoAlgarismo);

                ValidarIgualdadeParaProsseguir(algarismoAtual, proximoAlgarismo);

                AvaliarSeHaSubtracaoPosRepeticao(algarismoAtual, proximoAlgarismo);
            }
        }

        private static void AplicarValidacaoSubtracao(Algarismo algarismoAtual, Algarismo proximoAlgarismo)
        {
            if (_estado.ExisteSubtracao)
            {
                if (_regraSubtracao.ChecarSubtracao(algarismoAtual, proximoAlgarismo))
                {
                    _estado.PonteiroAtual++;
                    _estado.PonteiroSeguinte++;
                    return;
                }

                AvaliarSeHaDecrescenciaPosSubtracao(algarismoAtual, proximoAlgarismo);
            }
        }
        
        private static void AvaliarSeHaDecrescenciaPosSubtracao(Algarismo algarismoAtual, Algarismo proximoAlgarismo)
        {
            bool temDecrescencia = _regraDecrescencia.ChecarDecrescencia(algarismoAtual, proximoAlgarismo);
            if (temDecrescencia)
            {
                _regraSubtracao.ChecarPosterioresDaSubtracao(algarismoAtual, proximoAlgarismo);
            }

            if (!temDecrescencia)
            {
                _estado.Reiniciar();
                throw new NumeralInvalidoException($"O numeral infringe as regras de subtração.");
            }
        }

        private static void ValidarIgualdadeParaProsseguir(Algarismo algarismoAtual, Algarismo proximoAlgarismo)
        {
            _estado.EstaIgual = _regraIgualdade.ChecarIgualdade(algarismoAtual, proximoAlgarismo);
            if (_estado.EstaIgual)
            {
                _estado.Repetido += ControlarRepeticao(_estado.EstaIgual);
                _estado.PonteiroAtual++;
                _estado.PonteiroSeguinte++;
            }
        }

        private static void AvaliarSeHaSubtracaoPosRepeticao(Algarismo algarismoAtual, Algarismo proximoAlgarismo)
        {
            _estado.ExisteSubtracao = _regraSubtracao.ChecarSubtracao(algarismoAtual, proximoAlgarismo);
            if (_estado.ExisteSubtracao)
            {
                _estado.Reiniciar();
                throw new NumeralInvalidoException($"O numeral apresenta uma subtração após uma repetição de um algarismo.");
            }
        }

        private static int ControlarRepeticao(bool igual)
        {
            return igual ? 1 : 0;
        }

        private static void ChecarLimiteRepeticao(int repetido)
        {
            if (repetido == 3)
            {
                _estado.Reiniciar();
                throw new NumeralInvalidoException($"O numeral possui mais de três algarismos iguais.");
            }
        }

        private static void ChecarValidadeDaRepeticao(int repetido, Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            bool existeSubtracao = _regraSubtracao.ChecarSubtracao(primeiroAlgarismo, segundoAlgarismo);
            if (repetido == 1 && existeSubtracao)
            {
                _estado.Reiniciar();
                throw new NumeralInvalidoException($"Algarismos 'V', 'L', 'D' não podem ser repetidos.");
            }
        }

        private static void AvaliarRepeticaoUnicaComDecrescencia(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            bool decrescente = _regraDecrescencia.ChecarDecrescencia(primeiroAlgarismo, segundoAlgarismo);
            if (_estado.Repetido == 1 && decrescente)
            {
                _estado.Repetido = 0;
            }
        }

        private static void AvaliarRepeticaoDuplaComDecrescencia(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            bool decrescente = _regraDecrescencia.ChecarDecrescencia(primeiroAlgarismo, segundoAlgarismo);
            if (_estado.Repetido == 2 && decrescente)
            {
                _estado.Repetido = 0;
            }
        }

        private static void ChecarBlocoRepeticao()
        {
            _estado.BlocoRepeticaoCompleto = (_estado.Repetido == 2);
        }

        private static void VerificarPosterioridadeDoBlocoRepeticao(Algarismo primeiroAlgarismo, Algarismo segundoAlgarismo)
        {
            if (_estado.BlocoRepeticaoCompleto)
            {
                bool decrescente = _regraDecrescencia.ChecarDecrescencia(primeiroAlgarismo, segundoAlgarismo);
                if (!decrescente)
                {
                    _estado.Reiniciar();
                    throw new NumeralInvalidoException($"O numeral possui mais de três algarismos iguais.");
                }
                _estado.Repetido = 0;
            }
        }

        internal static void ValidarCadaSimboloDoNumeral(string inputNumeralUsuario)
        {
            foreach (var letra in inputNumeralUsuario)
            {
                ValidarSimbolo(letra);
            }
        }

        private static void ValidarSimbolo(char letra)
        {
            Simbolo simboloUsuario = Simbolo.GetSimbolo(letra)!;
            if (simboloUsuario == null)
            {
                throw new NumeralInvalidoException($"{char.ToUpper(letra)} não é um Algarismo Romano!");
            }
        }
    }
}