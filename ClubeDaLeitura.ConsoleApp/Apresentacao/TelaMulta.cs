using System;
using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaMulta : ITela
{
    private RepositorioMulta repositorioMulta;

    public TelaMulta(RepositorioMulta repositorioMulta)
    {
        this.repositorioMulta = repositorioMulta;
    }

    public string? ObterOpcaoMenu()
    {
        // Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de emprestimo");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Visualizar Multas em Aberto");
        Console.WriteLine($"2 - Pagar Multa");
        Console.WriteLine($"3 - Visualizar Multas de Um amigo");
        Console.WriteLine($"S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    internal void Quitar()
    {
        VisualizarTodos(deveExibirCabecalho: false);

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID da Multa para quitar : ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        Multa multaSelecionada = (Multa)repositorioMulta.SelecionarPorId(idSelecionado);

        if (multaSelecionada == null)
        {
            ExibirMensagem("ID da Multa não encontrado, tente novamente.");
            return;
        }

        System.Console.Write("Deseja pagar a multa do amigo " + multaSelecionada.emprestimo.Amigo.Nome + " no valor de R$" + multaSelecionada.Valor + "? (S/N) ");
        string? resposta = Console.ReadLine()?.ToUpper();
        if (resposta == "S")
        {
            multaSelecionada.Status = StatusMulta.Quitado;
            ExibirMensagem("Multa paga com sucesso!");
            repositorioMulta.Excluir(idSelecionado);
        }
        else
        {
            ExibirMensagem("Operação cancelada.");
        }

    }
    public void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Multas");

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -15} | {3, -13}",
            "Id", "Amigo", "Revista", "Valor Multa"
        );

        EntidadeBase?[] listaMultas = repositorioMulta.SelecionarTodas();

        for (int i = 0; i < listaMultas.Length; i++)
        {
            Multa? a = (Multa?)listaMultas[i];

            if (a == null)
                continue;
            Console.WriteLine(
                "{0, -7} | {1, -15} | {2, -15} | {3, -13}",
                a.Id, a.emprestimo.Amigo.Nome, a.emprestimo.Revista.Titulo, "R$" + a.Valor
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }
    internal void VisualizarTodos()
    {
        EntidadeBase?[] rep = repositorioMulta.VisualizarTodos();
    }

    internal void VisualizarAmigo(bool deveExibirCabecalho)
    {
        throw new NotImplementedException();
    }

    protected void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Multas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(titulo);
        Console.WriteLine("---------------------------------");
    }

    protected static void ExibirMensagem(string mensagem)
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }
}
