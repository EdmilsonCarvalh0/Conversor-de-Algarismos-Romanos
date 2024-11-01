using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Models
{
    public class Numeral
    {
        public bool Valido { get; set; }
        public List<Algarismo> AlgarismosDoNumeral { get; private set; }

        public Numeral()
        {
            AlgarismosDoNumeral = new List<Algarismo>();
        }

        internal void AdicionarAlgarismosNoNumeralValido(List<Algarismo> algarismos)
        {
            AlgarismosDoNumeral = algarismos;
            TornarValido();
        }

        internal void TornarValido()
        {
            Valido = true;
        }
    }
}
