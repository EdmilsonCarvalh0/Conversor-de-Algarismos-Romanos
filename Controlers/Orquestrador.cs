﻿using ConversorAlgarismoRomano.Exceptions;
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
        private readonly EntradaUsuario _entradaUsuario;

        public Orquestrador(ConversorService conversor)
        {
            _conversor = conversor;
            _entradaUsuario = new EntradaUsuario();
        }

        public void Executar()
        {
            while (true)
            {
                try
                {
                    string inputNumeral = _entradaUsuario.GetNumeralUsuario();

                    _conversor.PrepararParaConversao(inputNumeral);

                    int resultado = _conversor.Converter();

                    _entradaUsuario.ExibirResultado(resultado);
                    break;
                }
                catch (NumeralInvalidoException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
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
