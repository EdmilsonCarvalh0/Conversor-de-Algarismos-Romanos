using ConversorAlgarismoRomano.Models;
using ConversorAlgarismoRomano.Rules.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Rules
{
    internal interface IRegraValidacao
    {
        public interface IRegraValidacao
        {
            bool Validar(Algarismo algarismoAtual, Algarismo proximoAlgarismo);
        }
    }
}