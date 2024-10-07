using ConversorAlgarismoRomano.Exceptions;
using ConversorAlgarismoRomano.InputHandlers;
using ConversorAlgarismoRomano.Models;
using ConversorAlgarismoRomano.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Controlers
{
    internal class Orquestrador
    {
        private static ConversorService _conversor {  get; set; } = new ConversorService();

        public Orquestrador(ConversorService conversor)
        {
            _conversor = conversor;
        }

        public void Executar()
        {
            string inputNumeral = EntradaUsuario.GetNumeralUsuario();

            try
            {
                int resultado = _conversor.Converter(inputNumeral);

                EntradaUsuario.ExibirResultado(resultado);
            }
            catch (NumeralInvalidoException ex)
            {
                Console.WriteLine(ex.Message);
                Executar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }
    }
}
