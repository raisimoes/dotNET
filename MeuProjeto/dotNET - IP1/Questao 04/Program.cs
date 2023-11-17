using System;
using System.Diagnostics;

class Program {
    static void Main(){
        // Declaração e inicialização de variáveis
        int x = 10;
        int y = 3;
        
        // Adição
        int Adicao = x + y;
        Console.WriteLine($"Adição: {Adicao}");

        //Subtração
        int Subtracao = x - y;
        Console.WriteLine($"Subtração: {Subtracao}");

        //Multiplicação
        int Multiplicacao = x * y;
        Console.WriteLine($"Multiplicação {Multiplicacao}");

        //Divisão
        int Divisao = x / y;
        Console.WriteLine($"Divisão: {Divisao}");
        /*O resultado será inteiro pois x e y são inteiros, para obter um resultado
        decimal, x ou y podem ser convertidos para double antes da divisão:*/
        double DivisaoDecimal = (double)x / y;
        Console.WriteLine($"Divisão com resultado decimal: {DivisaoDecimal}");
    }
}