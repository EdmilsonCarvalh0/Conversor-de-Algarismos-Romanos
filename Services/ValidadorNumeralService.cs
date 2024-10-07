using ConversorAlgarismoRomano.Exceptions;
using ConversorAlgarismoRomano.InputHandlers;
using ConversorAlgarismoRomano.Models;
using ConversorAlgarismoRomano.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Services
{
    public class ValidadorNumeralService
    {
        public static Numeral Numeral {  get; set; }

        static ValidadorNumeralService()
        {
            Numeral = Numeral.GetNumeral();
        }

        public Numeral ValidarNumeral(string inputNumeralUsuario)
        {
            GerenciadorDeValidacoes.ValidarCadaSimboloDoNumeral(inputNumeralUsuario);
            List<Algarismo> algarismosAValidar = PrepararAlgarismosParaValidação(inputNumeralUsuario);
            GerenciadorDeValidacoes.Validar(algarismosAValidar);
            List<Algarismo> algarismosValidos = algarismosAValidar;
            Numeral.AdicionarAlgarismosNoNumeralValido(algarismosValidos);

            return Numeral;
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
    }
}
