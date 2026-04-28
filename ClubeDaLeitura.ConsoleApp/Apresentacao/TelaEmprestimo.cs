using System;
using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaEmprestimo : ITela
{
    private RepositorioEmprestimo repositorioEmprestimo;
    private RepositorioRevista repositorioRevista;
    private RepositorioAmigo RepositorioAmigo;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioRevista repositorioRevista, RepositorioAmigo repositorioAmigo)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioRevista = repositorioRevista;
        RepositorioAmigo = repositorioAmigo;
    }

    public string ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de emprestimo");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Abrir emprestimo");
        Console.WriteLine($"2 - Concluir emprestimo");
        Console.WriteLine($"3 - Visualizar emprestimos");
        Console.WriteLine($"S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void Abrir()
    {
        //logica de abertura de cadastro de emprestimo
        //1. obter os dados obrigatorios revista e amigo
        Emprestimo emprestimo = ObterDadosCadastrais();
        //2. validar o emprestimo
        string[] erros = emprestimo.Validar();


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

            Abrir();
            return;
        }

        //3.Abrir o emprestimo
        emprestimo.Abrir();

        //4. armazenar essa porra
        repositorioEmprestimo.Cadastrar(emprestimo);
    }

    public void Concluir()
    {
        ExibirCabecalho("Conclusao de Emprestimo");

        VisualizarTodos(false);

        Emprestimo? e = null;
        do
        {
            Console.Write("Digite o ID do Emprestimo que deseja concluir: ");
            string idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
            {
                e = repositorioEmprestimo.SelecionarPorId(idSelecionado);
                break;
            }
        } while (e == null);

        Console.WriteLine("---------------------------------");
        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -10} | {3, -10} | {4, -10}",
            "Id", "Revista", "Amigo", "Inicio", "Conclusao prev"
        );
        Console.WriteLine(
                   "{0, -7} | {1, -15} | {2, -10} | {3, -10} | {4, -10}",
                   e.Id, e.Revista.Titulo, e.Amigo.Nome, e.Abertura.ToShortDateString(), e.ConclusaoPrevista.ToShortDateString()
               );
        Console.WriteLine("---------------------------------");
        System.Console.Write("Deseja concluir o emprestimo selecionado? (s/n)");
        string? opcaoContinuar = Console.ReadLine()?.ToUpper();

        if (opcaoContinuar != "S")
        {
            System.Console.WriteLine("-----------------------");
            System.Console.WriteLine("Digite ENTER para continuar!");
            Console.ReadLine();
            Console.Clear();
            return;
        }

        e.Concluir();

        ExibirMensagem($"O emprestimo {e.Id} foi concluido com sucesso!");
    }
    public void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Emprestimo");

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -10} | {3, -10} | {4, -10} | {5, -7} |",
            "Id", "Revista", "Amigo", "Inicio", "Conclusao prev", "Status"
        );

        Emprestimo?[] emprestimos = repositorioEmprestimo.SelecionarTodas();

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo? a = emprestimos[i];

            if (a == null)
                continue;


            Console.Write("{0, -7} | ", a.Id);
            Console.Write("{0, -15} | ", a.Revista.Titulo);
            Console.Write("{0, -10} | ", a.Amigo.Nome);
            Console.Write("{0, -10} | ", a.Abertura.ToShortDateString());
            Console.Write("{0, -10} | ", a.ConclusaoPrevista.ToShortDateString());

            string status = a.Status.ToString();
            if (a.EstaAtrasado)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                status = "Atrasado";
            }
            else if (a.Status == StatusEmprestimo.Indefinido)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (a.Status == StatusEmprestimo.Aberto)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (a.Status == StatusEmprestimo.Concluido)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write("{0, -7} | ", a.Status);

            Console.ResetColor();
            System.Console.WriteLine();
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }
    private Emprestimo ObterDadosCadastrais()
    {
        VisualizarRevistas();

        Revista? revista = null;

        do
        {
            Console.Write("Digite o ID do registro da revista que deseja selecionar: ");
            string? idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
            {
                revista = (Revista)repositorioRevista.SelecionarPorId(idSelecionado);
                break;
            }
        } while (revista == null);
        Console.WriteLine("---------------------------------");

        VisualizarAmigos();

        Amigo? amigo = null;

        do
        {
            Console.Write("Digite o ID do registro do amigo que recebera a revista: ");
            string? idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
            {
                amigo = (Amigo)RepositorioAmigo.SelecionarPorId(idSelecionado);
                break;
            }
        } while (amigo == null);

        return new Emprestimo(revista, amigo);
    }
    private void VisualizarRevistas()
    {
        Console.WriteLine(
            "{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -20}",
            "Id", "Titulo", "Numero da Edição", "Ano de Publicação", "Caixa"
        );

        EntidadeBase?[] revistas = repositorioRevista.SelecionarTodas();

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
        Console.WriteLine("---------------------------------");
    }
    private void VisualizarAmigos()
    {

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -15} | {3, -13}",
            "Id", "Nome", "Responsável", "Telefone"
        );

        EntidadeBase?[] amigos = RepositorioAmigo.SelecionarTodas();

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo? a = (Amigo?)amigos[i];

            if (a == null)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -15} | {2, -15} | {3, -13}",
                a.Id, a.Nome, a.NomeResponsavel, a.Telefone
            );
        }
        Console.WriteLine("---------------------------------");
    }
    private void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Emprestimo");
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
