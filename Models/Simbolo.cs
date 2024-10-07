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

        public static readonly Simbolo i = new Simbolo('I');
        public static readonly Simbolo v = new Simbolo('V');
        public static readonly Simbolo x = new Simbolo('X');
        public static readonly Simbolo l = new Simbolo('L');
        public static readonly Simbolo c = new Simbolo('C');
        public static readonly Simbolo d = new Simbolo('D');
        public static readonly Simbolo m = new Simbolo('M');

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
