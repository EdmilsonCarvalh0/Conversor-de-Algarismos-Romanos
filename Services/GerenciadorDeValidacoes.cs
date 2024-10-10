using ConversorAlgarismoRomano.Exceptions;
using ConversorAlgarismoRomano.Interfaces;
using ConversorAlgarismoRomano.Models;
using ConversorAlgarismoRomano.Rules.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Services
{
    internal class GerenciadorDeValidacoes : IGerenciadorDeValidacoes
    {
        public Numeral Numeral { get; set; }
        private readonly ValidadorNumeralService _validador;

        public GerenciadorDeValidacoes()
        {
            _validador = new ValidadorNumeralService();
            Numeral = new Numeral();
        }

        public Numeral IniciarValidacao(string inputNumeralUsuario)
        {
            _validador.ValidarSimbolosDoNumeral(inputNumeralUsuario);
            List<Algarismo> algarismosAValidar = PrepararAlgarismosParaValidação(inputNumeralUsuario);
            _validador.Validar(algarismosAValidar);
            List<Algarismo> algarismosValidos = algarismosAValidar;
            Numeral.AdicionarAlgarismosNoNumeralValido(algarismosValidos);

            return Numeral;
        }

        private static List<Algarismo> PrepararAlgarismosParaValidação(string inputNumeralUsuario)
        {
            List<Algarismo> algarismosAVerificar = new List<Algarismo>();

            foreach (char letra in inputNumeralUsuario)
            {
                Simbolo simboloUsuario = Simbolo.GetSimbolo(letra)!;
                Algarismo algarismo = Algarismo.GetAlgarismo(simboloUsuario)!;
                algarismosAVerificar.Add(algarismo);
            }
            return algarismosAVerificar;
        }








        //private bool Validar(List<Algarismo> algarismosAVerificar)
        //{
        //    foreach (var regra in _regras)
        //    {
        //        if (!regra.Aplicar(algarismosAVerificar))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}