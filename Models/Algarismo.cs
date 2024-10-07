using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Models
{
    public class Algarismo
    {
        public Simbolo? Simbolo { get; private set; }
        public int ValorCorrespondente { get; private set; }

        public static readonly Algarismo i = new();
        public static readonly Algarismo v = new();
        public static readonly Algarismo x = new();
        public static readonly Algarismo l = new();
        public static readonly Algarismo c = new();
        public static readonly Algarismo d = new();
        public static readonly Algarismo m = new();

        public static ImmutableList<Algarismo> Algarismos { get; private set; }

        static Algarismo()
        {
            i.Simbolo = Simbolo.GetSimbolo('i');
            i.ValorCorrespondente = 1;
            v.Simbolo = Simbolo.GetSimbolo('v');
            v.ValorCorrespondente = 5;
            x.Simbolo = Simbolo.GetSimbolo('x');
            x.ValorCorrespondente = 10;
            l.Simbolo = Simbolo.GetSimbolo('l');
            l.ValorCorrespondente = 50;
            c.Simbolo = Simbolo.GetSimbolo('c');
            c.ValorCorrespondente = 100;
            d.Simbolo = Simbolo.GetSimbolo('d');
            d.ValorCorrespondente = 500;
            m.Simbolo = Simbolo.GetSimbolo('m');
            m.ValorCorrespondente = 1000;

            Algarismos = ImmutableList.Create(i, v, x, l, c, d, m);
        }

        public static Algarismo? GetAlgarismo(Simbolo s)
        {
            return Algarismos.FirstOrDefault(algarismo => algarismo.Simbolo!.Equals(s));
        }

        public override bool Equals(object? obj)
        {
            if (obj is Algarismo algarismo)
            {
                return Simbolo!.Equals(algarismo.Simbolo);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Simbolo!.Letra.GetHashCode();
        }
    }
}