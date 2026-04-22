using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaRevista : TelaBase
{
    private RepositorioRevista RepositorioRevista;
    private RepositorioCaixa RepositorioCaixa;

    public TelaRevista(RepositorioCaixa rc, RepositorioRevista rv) : base("Revista", rv)
    {
        RepositorioCaixa = rc;
        RepositorioRevista = rv;
    }

    public void Cadastrar()
    {
        ExibirCabecalho("Cadastro de Revista");

        EntidadeBase? novaRevista = ObterDadosCadastrais();

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
        RepositorioRevista.Cadastrar(novaRevista);

        ExibirMensagem("Revista cadastrada com sucesso!");
    }

    protected override EntidadeBase ObterDadosCadastrais()
    {
        System.Console.Write("Digite o Titulo da revista: ");
        string? titulo = Console.ReadLine();

        System.Console.Write("Digite o numero da edicao: ");
        int numeroEdicao = Convert.ToInt32(Console.ReadLine());

        System.Console.Write("Digite o numero da Ano de Publicacao: ");
        int AnoPublicacao = Convert.ToInt32(Console.ReadLine());

        string idSelecionado = VisualizarESelecionarCaixa();

        Caixa? caixaSelecionada = (Caixa?)RepositorioCaixa.SelecionarPorId(idSelecionado);

        return new Revista(titulo, numeroEdicao, AnoPublicacao, caixaSelecionada);

    }
    private string VisualizarESelecionarCaixa()
    {
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -10} | {3, -20}",
            "Id", "Etiqueta", "Cor", "Tempo de Empréstimo"
        );

        EntidadeBase?[] caixas = RepositorioCaixa.SelecionarTodas();

        for (int i = 0; i < caixas.Length; i++)
        {
            Caixa? c = (Caixa?)caixas[i];

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
            Console.Write("Digite o ID do registro : ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        return idSelecionado;
    }
    
    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Caixas");

        Console.WriteLine(
            "{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -20}",
            "Id", "Titulo", "Numero da Edição", "Ano de Publicação", "Caixa"
        );

        EntidadeBase?[] revistas = RepositorioRevista.SelecionarTodas();

        for (int i = 0; i < revistas.Length; i++)
        {
            Revista? r = (Revista?)revistas[i];

            if (r == null)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -20}",
                r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa.Etiqueta
            );
        }
        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }



}
