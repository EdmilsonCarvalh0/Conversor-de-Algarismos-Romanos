using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Exceptions
{
    internal class NumeralInvalidoException : Exception
    {
        public NumeralInvalidoException(string mensagem) : base(mensagem) { }
    }
}
