using ConversorAlgarismoRomano.Controlers;
using ConversorAlgarismoRomano.InputHandlers;
using ConversorAlgarismoRomano.Models;
using ConversorAlgarismoRomano.Services;

namespace ConversorAlgarismoRomano
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region PROPOSTA
            /*
            Algarismos romanos são representados por 7 símbolos diferentes:  I, V, X, L, C, D e M.

            Valor do símbolo:
            I 1
            V 5
            X 10
            E 50
            C 100
            D 500
            M 1000

            Por exemplo,  2 é escrito como II em numeral romano, apenas dois uns somados. 12 é
            escrito como XII, que é simplesmente X + II. O número 27 é escrito como XXVII, que
            é XX + V + II.

            Os algarismos romanos são geralmente escritos do maior para o menor, da esquerda para
            a direita. No entanto, o numeral para quatro não é IIII. Em vez disso, o número quatro
            é escrito como IV. Como o um está antes do cinco, nós o subtraímos, resultando em
            quatro. O mesmo princípio se aplica ao número nove, que é escrito como IX. Há seis
            instâncias em que a subtração é usada:

                I pode ser colocado antes de V(5) e X(10) para formar 4 e 9. 
                X pode ser colocado antes de L(50) e C(100) para fazer 40 e 90. 
                C pode ser colocado antes de D(500) e M(1000) para fazer 400 e 900.

            Dado um numeral romano, converta-o em um inteiro. 

            Exemplo 1:
             Entrada: s = "III"
              Saída: 3
              Explicação: III = 3.

            Exemplo 2:
             Entrada: s = "LVIII"
              Saída: 58
              Explicação: L = 50, V= 5, III = 3.

            Exemplo 3:
            Entrada: s = "MCMXCIV"
              Saída: 1994
              Explicação: M = 1000, CM = 900, XC = 90 e IV = 4.
 
            Restrições:
             1 <= s.length <= 15
             s contém apenas os caracteres ('I', 'V', 'X', 'L', 'C', 'D', 'M').
             É garantido  que sé um numeral romano válido no intervalo [1, 3999]. 
            */
            #endregion

            ConversorService conversor = new ConversorService();
            Orquestrador orquestrador = new Orquestrador(conversor);

            orquestrador.Executar();
        }
    }
}
