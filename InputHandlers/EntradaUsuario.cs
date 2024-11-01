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
        private string InputNumeralUsuario { get; set; } = "";

        public static Numeral Numeral { get; private set; } = new Numeral();

        public string GetNumeralUsuario()
        {
            Console.WriteLine("Informe um numeral romano para converter: ");
            InputNumeralUsuario = Console.ReadLine()!;

            bool inputVazio = VerificarInput();
            if (inputVazio) ReiniciarEntradaUsuario();

            return InputNumeralUsuario;
        }
        private bool VerificarInput()
        {
            return string.IsNullOrEmpty(InputNumeralUsuario);
        }

        private void ReiniciarEntradaUsuario()
        {
            Console.WriteLine("Insira um numeral.");
            GetNumeralUsuario();
            Console.Clear();
        }

        public void ExibirResultado(int resultado)
        {
            Console.WriteLine($"O numeral {InputNumeralUsuario.ToUpper()} tem como valor {resultado}.");
        }
    }
}
