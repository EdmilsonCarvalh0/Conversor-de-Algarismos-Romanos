﻿using ConversorAlgarismoRomano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Rules
{
    internal class EstadoValidacao
    {
        public int QuantidadeDeVerificacoes { get; set; }
        public bool EstaDecrescente { get; set; }
        public bool EstaSubtraindo { get; set; }
        public bool EstaIgual { get; set; }
        public bool BlocoRepeticaoCompleto { get; set; }
        public int Repetido { get; set; }
        public int PonteiroAtual { get; set; }
        public int PonteiroSeguinte { get; set; }

        public EstadoValidacao()
        {
            QuantidadeDeVerificacoes = 1;
            EstaDecrescente = false;
            EstaSubtraindo = false;
            EstaIgual = false;
            BlocoRepeticaoCompleto = false;
            Repetido = 0;
            PonteiroAtual = 0;
            PonteiroSeguinte = 1;
        }

        internal void ContabilizarValidacoes(List<Algarismo> algarismos)
        {
            if (algarismos.Count > 1)
            {
                QuantidadeDeVerificacoes = algarismos.Count-1;
            }

            if (algarismos.Count == 1)
            {
                QuantidadeDeVerificacoes = 0;
            }
        }

        internal void ControlarBlocoRepeticao()
        {
            if (BlocoRepeticaoCompleto)
            {
                ReiniciarEstado();
                PonteiroAtual++;
                PonteiroSeguinte++;
            }
        }

        internal void ReiniciarEstado()
        {
            EstaDecrescente = false;
            EstaSubtraindo = false;
            EstaIgual = false;
            BlocoRepeticaoCompleto = false;
            Repetido = 0;
            PonteiroAtual = 0;
            PonteiroSeguinte = 1;
        }
    }
}
