using System;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaRevista
{
    private RepositorioCaixa RepositorioCaixa;

    public TelaRevista(RepositorioCaixa rc)
    {
        RepositorioCaixa = rc;
    }
    public string ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Revista");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Cadastrar Revista");
        Console.WriteLine("2 - Editar Revista");
        Console.WriteLine("3 - Excluir Revista");
        Console.WriteLine("4 - Visualizar Revista");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }
    public void Cadastrar()
    {
        ExibirCabecalho("Cadastro de Revista");

        Revista novaRevista = ObterDadosCadastrais();

        string?[] erros = novaRevista.Validar();


        if (erros.Length > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (string s in erros)
            {
                System.Console.WriteLine(s);
            }
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
            Console.ResetColor();

            Cadastrar();

            return;
        }
    }

    private Revista ObterDadosCadastrais()
    {
        System.Console.Write("Digite o Titulo da revista: ");
        string? titulo = Console.ReadLine();

        System.Console.Write("Digite o numero da edicao: ");
        int numeroEdicao = Convert.ToInt32(Console.ReadLine());

        System.Console.Write("Digite o numero da Ano de Publicacao: ");
        int AnoPublicacao = Convert.ToInt32(Console.ReadLine());

        string idSelecionado = VisualizarESelecionarCaixa();

        Caixa? caixaSelecionada = RepositorioCaixa.SelecionarPorId(idSelecionado);

        return new Revista(titulo, numeroEdicao, AnoPublicacao, caixaSelecionada);

    }
    private string VisualizarESelecionarCaixa()
    {
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -10} | {3, -20}",
            "Id", "Etiqueta", "Cor", "Tempo de Empréstimo"
        );

        Caixa?[] caixas = RepositorioCaixa.SelecionarTodas();

        for (int i = 0; i < caixas.Length; i++)
        {
            Caixa? c = caixas[i];

            if (c == null)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -10} | {3, -20}",
                c.Id, c.Etiqueta, c.Cor, c.DiasDeEmprestimo
            );
        }
        System.Console.WriteLine("--------------------");
        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja excluir: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        return idSelecionado;
    }
    public void Editar()
    {

    }
    public void Excluir()
    {

    }
    public void VisualizarTodos(bool deveExibirCabecalho)
    {

    }

    private void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Revistas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(titulo);
        Console.WriteLine("---------------------------------");
    }

    private static void ExibirMensagem(string mensagem)
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }

}
