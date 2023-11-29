using System;
using System.Collections.Generic;
using System.Linq;

public record Produto(int Codigo, string Nome, int QuantidadeEmEstoque, decimal PrecoUnitario);

public class ProdutoNaoEncontradoException : Exception
{
    public ProdutoNaoEncontradoException(string mensagem) : base(mensagem)
    {
    }
}

public class Estoque
{
    private List<Produto> produtos = new List<Produto>();

    public void CadastrarProduto(int codigo, string nome, int quantidade, decimal preco)
    {
        try
        {
            if (produtos.Any(p => p.Codigo == codigo))
                throw new InvalidOperationException("Código de produto já existente.");

            Produto novoProduto = new Produto(codigo, nome, quantidade, preco);
            produtos.Add(novoProduto);
            Console.WriteLine("Produto cadastrado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao cadastrar produto: {ex.Message}");
        }
    }

    public Produto ConsultarProduto(int codigo)
    {
        Produto produtoEncontrado = produtos.FirstOrDefault(p => p.Codigo == codigo);

        if (produtoEncontrado == null)
            throw new ProdutoNaoEncontradoException("Produto não encontrado.");

        return produtoEncontrado;
    }

    public void AtualizarEstoque(int codigo, int quantidade)
    {
        try
        {
            Produto produto = ConsultarProduto(codigo);

            if (quantidade < 0 && Math.Abs(quantidade) > produto.QuantidadeEmEstoque)
                throw new InvalidOperationException("Quantidade em estoque insuficiente para a saída.");

            // Atualiza a lista de produtos diretamente
            produtos = produtos.Select(p =>
                p.Codigo == codigo
                    ? p with { QuantidadeEmEstoque = p.QuantidadeEmEstoque + quantidade }
                    : p).ToList();

            Console.WriteLine("Estoque atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar estoque: {ex.Message}");
        }
    }

    public void RelatorioQuantidadeAbaixoLimite(int limite)
    {
        var produtosAbaixoLimite = produtos.Where(p => p.QuantidadeEmEstoque < limite);
        Console.WriteLine($"Produtos com quantidade abaixo de {limite}:\n");

        foreach (var produto in produtosAbaixoLimite)
        {
            Console.WriteLine($"Código: {produto.Codigo}, Nome: {produto.Nome}, Quantidade: {produto.QuantidadeEmEstoque}, Preço: {produto.PrecoUnitario:C}");
        }
    }

    public void RelatorioValorEntreMinMax(decimal min, decimal max)
    {
        var produtosNoIntervalo = produtos.Where(p => p.PrecoUnitario >= min && p.PrecoUnitario <= max);
        Console.WriteLine($"Produtos com valor entre {min:C} e {max:C}:\n");

        foreach (var produto in produtosNoIntervalo)
        {
            Console.WriteLine($"Código: {produto.Codigo}, Nome: {produto.Nome}, Quantidade: {produto.QuantidadeEmEstoque}, Preço: {produto.PrecoUnitario:C}");
        }
    }

    public void RelatorioValorTotalEstoque()
    {
        decimal valorTotalEstoque = produtos.Sum(p => p.QuantidadeEmEstoque * p.PrecoUnitario);
        Console.WriteLine($"Valor total do estoque: {valorTotalEstoque:C}\n");

        foreach (var produto in produtos)
        {
            decimal valorTotalProduto = produto.QuantidadeEmEstoque * produto.PrecoUnitario;
            Console.WriteLine($"Código: {produto.Codigo}, Nome: {produto.Nome}, Valor total: {valorTotalProduto:C}");
        }
    }
}

class Program
{
    static void Main()
    {
        Estoque estoque = new Estoque();

        while (true)
        {
            Console.WriteLine("\n1. Cadastrar Produto");
            Console.WriteLine("2. Consultar Produto");
            Console.WriteLine("3. Atualizar Estoque");
            Console.WriteLine("4. Relatório: Quantidade abaixo de um limite");
            Console.WriteLine("5. Relatório: Valor entre mínimo e máximo");
            Console.WriteLine("6. Relatório: Valor total do estoque");
            Console.WriteLine("7. Sair");

            Console.Write("\nEscolha uma opção (1-7): ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Digite o código do produto: ");
                    int codigo = int.Parse(Console.ReadLine());

                    Console.Write("Digite o nome do produto: ");
                    string nome = Console.ReadLine();

                    Console.Write("Digite a quantidade em estoque: ");
                    int quantidade = int.Parse(Console.ReadLine());

                    Console.Write("Digite o preço unitário: ");
                    decimal preco = decimal.Parse(Console.ReadLine());

                    estoque.CadastrarProduto(codigo, nome, quantidade, preco);
                    break;

                case "2":
                    Console.Write("Digite o código do produto: ");
                    int codigoConsulta = int.Parse(Console.ReadLine());

                    try
                    {
                        var produtoConsultado = estoque.ConsultarProduto(codigoConsulta);
                        Console.WriteLine($"Código: {produtoConsultado.Codigo}, Nome: {produtoConsultado.Nome}, Quantidade: {produtoConsultado.QuantidadeEmEstoque}, Preço: {produtoConsultado.PrecoUnitario:C}");
                    }
                    catch (ProdutoNaoEncontradoException ex)
                    {
                        Console.WriteLine($"Erro: {ex.Message}");
                    }
                    break;

                case "3":
                    Console.Write("Digite o código do produto: ");
                    int codigoAtualizacao = int.Parse(Console.ReadLine());

                    Console.Write("Digite a quantidade a ser atualizada (negativa para saída, positiva para entrada): ");
                    int quantidadeAtualizacao = int.Parse(Console.ReadLine());

                    estoque.AtualizarEstoque(codigoAtualizacao, quantidadeAtualizacao);
                    break;

                case "4":
                    Console.Write("Digite o limite de quantidade: ");
                    int limiteQuantidade = int.Parse(Console.ReadLine());

                    estoque.RelatorioQuantidadeAbaixoLimite(limiteQuantidade);
                    break;

                case "5":
                    Console.Write("Digite o valor mínimo: ");
                    decimal minValor = decimal.Parse(Console.ReadLine());

                    Console.Write("Digite o valor máximo: ");
                    decimal maxValor = decimal.Parse(Console.ReadLine());

                    estoque.RelatorioValorEntreMinMax(minValor, maxValor);
                    break;

                case "6":
                    estoque.RelatorioValorTotalEstoque();
                    break;

                case "7":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
}
