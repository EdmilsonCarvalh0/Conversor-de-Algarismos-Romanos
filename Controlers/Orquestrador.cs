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
        private readonly ConversorService _conversor;

        public Orquestrador(ConversorService conversor)
        {
            _conversor = conversor;
        }

        public void Executar()
        {
            while (true)
            {
                string inputNumeral = EntradaUsuario.GetNumeralUsuario();

                try
                {
                    _conversor.PrepararParaConversao(inputNumeral);

                    int resultado = _conversor.Converter();

                    EntradaUsuario.ExibirResultado(resultado);
                    break;
                }
                catch (NumeralInvalidoException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                    break;
                }
            }
        }
    }
}
