using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Models
{
    public class Numeral
    {
        public bool Valido { get;  set; }
        public List<Algarismo> AlgarismosDoNumeral { get; private set; } = new List<Algarismo>();

        public Numeral()
        {
            AlgarismosDoNumeral = new List<Algarismo>();
        }

        public static Numeral GetNumeral()
        {
            return new Numeral();
        }

        internal void AdicionarAlgarismosNoNumeralValido(List<Algarismo> algarismos)
        {
            AlgarismosDoNumeral = algarismos;
            Valido = true;
        }
    }
}
