using ConversorAlgarismoRomano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Interfaces
{
    internal interface IGerenciadorDeValidacoes
    {
        Numeral IniciarValidacao(string inputNumeralUsuario);
    }
}
