using ConversorAlgarismoRomano.Models;
using ConversorAlgarismoRomano.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.InputHandlers
{
    public class EntradaUsuario
    {
        private string _inputUsuario = "";
        public string InputNumeralUsuario
        {
            get => _inputUsuario;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Por favor, insira um numeral.");
                }

                _inputUsuario = value;
            }
        }

        public string GetNumeralUsuario()
        {
            Console.WriteLine("Informe um numeral romano para converter: ");
            InputNumeralUsuario = Console.ReadLine()!;

            return InputNumeralUsuario;
        }

        public void ExibirResultado(int resultado)
        {
            Console.WriteLine($"O numeral {InputNumeralUsuario.ToUpper()} tem como valor {resultado}.");
        }
    }
}
