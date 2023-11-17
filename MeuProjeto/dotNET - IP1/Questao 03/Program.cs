/* Essa conversão pode ser feita através de um cast explícito.Porém, a parte fracionária
será truncada e o valor será arredonda para o valor inteiro mais próximo em relação a zero.
Se a magnitude da parte fracionária não puder ser representada como "int",
ela será completamente truncada. */

// Exemplo:
using System;

class MeuPrograma {
    static void Main(){
        // Variável do tipo double
        double ValorDouble = 19.32;

        // Convertendo o double para int usando um cast explícito
        int ValorInt = (int)ValorDouble;

        // Mostrando resultados

        Console.WriteLine($"Valor double: {ValorDouble}");
        Console.WriteLine($"Valor inteiro: {ValorInt}");
    }
}