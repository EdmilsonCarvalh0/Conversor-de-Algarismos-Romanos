using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConversorAlgarismoRomano.Models
{
    public class Simbolo
    {
        public char Letra { get; private set; }
        public bool Valido { get; private set; }

        public Simbolo(char letra)
        {
            Letra = char.ToUpper(letra);
            Valido = false;
        }

        public static readonly Simbolo i;
        public static readonly Simbolo v;
        public static readonly Simbolo x;
        public static readonly Simbolo l;
        public static readonly Simbolo c;
        public static readonly Simbolo d;
        public static readonly Simbolo m;

        public static ImmutableList<Simbolo> Simbolos { get; private set; }

        static Simbolo()
        {
            i = new Simbolo('I');
            i.Valido = true;
            v = new Simbolo('V');
            v.Valido = true;
            x = new Simbolo('X');
            x.Valido = true;
            l = new Simbolo('L');
            l.Valido = true;
            c = new Simbolo('C');
            c.Valido = true;
            d = new Simbolo('D');
            d.Valido = true;
            m = new Simbolo('M');
            m.Valido = true;

            Simbolos = ImmutableList.Create(i, v, x, l, c, d, m);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Simbolo simbolo)
            {
                return Letra == simbolo.Letra;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Letra.GetHashCode();
        }

        public static Simbolo? GetSimbolo(char letraCorrespondente)
        {
            char letraMaiuscula = char.ToUpper(letraCorrespondente);
            return Simbolos.FirstOrDefault(s => letraMaiuscula == s.Letra);
        }

        public void CheckSimbolo()
        {
            Valido = Simbolos.Any(s => Letra == s.Letra);
        }
    }
}
