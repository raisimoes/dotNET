using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Task> tasks = new List<Task>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("1. Criar Tarefa");
            Console.WriteLine("2. Listar Tarefas");
            Console.WriteLine("3. Marcar Tarefa como Concluída");
            Console.WriteLine("4. Listar Tarefas Pendentes");
            Console.WriteLine("5. Listar Tarefas Concluídas");
            Console.WriteLine("6. Excluir Tarefa");
            Console.WriteLine("7. Pesquisar Tarefas");
            Console.WriteLine("8. Exibir Estatísticas");
            Console.WriteLine("9. Sair");

            Console.Write("Escolha uma opção: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CriarTarefa();
                    break;
                case "2":
                    ListarTarefas();
                    break;
                case "3":
                    MarcarComoConcluida();
                    break;
                case "4":
                    ListarTarefasPendentes();
                    break;
                case "5":
                    ListarTarefasConcluidas();
                    break;
                case "6":
                    ExcluirTarefa();
                    break;
                case "7":
                    PesquisarTarefas();
                    break;
                case "8":
                    ExibirEstatisticas();
                    break;
                case "9":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.Clear();
        }
    }

    static void CriarTarefa()
    {
        Console.Write("Digite o título da tarefa: ");
        string titulo = Console.ReadLine();

        Console.Write("Digite a descrição da tarefa: ");
        string descricao = Console.ReadLine();

        Console.Write("Digite a data de vencimento (formato dd/mm/aaaa): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dataVencimento))
        {
            Task novaTarefa = new Task(titulo, descricao, dataVencimento);
            tasks.Add(novaTarefa);
            Console.WriteLine("Tarefa criada com sucesso!");
        }
        else
        {
            Console.WriteLine("Formato de data inválido. Tarefa não criada.");
        }

        Console.ReadLine();
    }

    static void ListarTarefas()
    {
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
            Console.WriteLine("------------");
        }

        Console.ReadLine();
    }

    static void MarcarComoConcluida()
    {
        ListarTarefas();
        Console.Write("Digite o número da tarefa a ser marcada como concluída: ");

        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= tasks.Count)
        {
            tasks[index - 1].Concluir();
            Console.WriteLine("Tarefa marcada como concluída!");
        }
        else
        {
            Console.WriteLine("Número de tarefa inválido.");
        }

        Console.ReadLine();
    }

    static void ListarTarefasPendentes()
    {
        var pendentes = tasks.Where(t => !t.Concluida).ToList();
        foreach (var task in pendentes)
        {
            Console.WriteLine(task);
            Console.WriteLine("------------");
        }

        Console.ReadLine();
    }

    static void ListarTarefasConcluidas()
    {
        var concluidas = tasks.Where(t => t.Concluida).ToList();
        foreach (var task in concluidas)
        {
            Console.WriteLine(task);
            Console.WriteLine("------------");
        }

        Console.ReadLine();
    }

    static void ExcluirTarefa()
    {
        ListarTarefas();
        Console.Write("Digite o número da tarefa a ser excluída: ");

        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= tasks.Count)
        {
            tasks.RemoveAt(index - 1);
            Console.WriteLine("Tarefa excluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Número de tarefa inválido.");
        }

        Console.ReadLine();
    }

    static void PesquisarTarefas()
    {
        Console.Write("Digite uma palavra-chave para a pesquisa: ");
        string keyword = Console.ReadLine().ToLower();

        var resultados = tasks.Where(t => t.Titulo.ToLower().Contains(keyword) || t.Descricao.ToLower().Contains(keyword)).ToList();

        if (resultados.Count > 0)
        {
            Console.WriteLine("Resultados da pesquisa:");
            foreach (var task in resultados)
            {
                Console.WriteLine(task);
                Console.WriteLine("------------");
            }
        }
        else
        {
            Console.WriteLine("Nenhum resultado encontrado.");
        }

        Console.ReadLine();
    }

    static void ExibirEstatisticas()
    {
        Console.WriteLine($"Número de tarefas concluídas: {tasks.Count(t => t.Concluida)}");
        Console.WriteLine($"Número de tarefas pendentes: {tasks.Count(t => !t.Concluida)}");

        if (tasks.Any())
        {
            Console.WriteLine($"Tarefa mais antiga: {tasks.Min(t => t.DataCriacao)}");
            Console.WriteLine($"Tarefa mais recente: {tasks.Max(t => t.DataCriacao)}");
        }
        else
        {
            Console.WriteLine("Nenhuma tarefa encontrada.");
        }

        Console.ReadLine();
    }
}

class Task
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; }
    public DateTime DataVencimento { get; set; }
    public bool Concluida { get; private set; }

    public Task(string titulo, string descricao, DateTime dataVencimento)
    {
        Titulo = titulo;
        Descricao = descricao;
        DataCriacao = DateTime.Now;
        DataVencimento = dataVencimento;
        Concluida = false;
    }

    public void Concluir()
    {
        Concluida = true;
    }

    public override string ToString()
    {
        return $"Título: {Titulo}\nDescrição: {Descricao}\nData de Criação: {DataCriacao}\nData de Vencimento: {DataVencimento}\nConcluída: {Concluida}";
    }
}