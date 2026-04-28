using System;
using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaPrincipal
{

    RepositorioCaixa repositorioCaixa;
    RepositorioRevista repositorioRevista;
    RepositorioAmigo repositorioAmigo;
    RepositorioEmprestimo repositorioEmprestimo;
    RepositorioMulta RepositorioMulta;

    public TelaPrincipal(
        RepositorioCaixa repositorioCaixa,
        RepositorioRevista repositorioRevista,
        RepositorioAmigo repositorioAmigo,
        RepositorioEmprestimo repositorioEmprestimo,
        RepositorioMulta repositorioMulta)
    {
        this.repositorioCaixa = repositorioCaixa;
        this.repositorioRevista = repositorioRevista;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioEmprestimo = repositorioEmprestimo;

        Caixa caixa = new Caixa("Lançamentos", "Vermelho", 3);
        repositorioCaixa.Cadastrar(caixa);
        Amigo amigo = new Amigo("João", "Maria", "49999999999");
        Revista revista = new Revista("Revista Super Interessante", 1, 2024, caixa);
        repositorioRevista.Cadastrar(revista);
        repositorioAmigo.Cadastrar(amigo);

        Emprestimo emprestimo = new(revista, amigo);
        repositorioEmprestimo.Cadastrar(emprestimo);
        emprestimo.Abrir();
        System.Console.WriteLine(emprestimo.Multa);
        RepositorioMulta = repositorioMulta;

        repositorioMulta.CadastrarMultas();
    }

    public ITela? ApresentarMenuOpcoesPrincipal()
    {
        // Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Clube da Leitura");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gerenciar caixas de revistas");
        Console.WriteLine("2 - Gerenciar revistas");
        Console.WriteLine("3 - Gerenciar amigos");
        Console.WriteLine("4 - Gerenciar empréstimos");
        System.Console.WriteLine("5 - Gerenciar Multas");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaCaixa(repositorioCaixa);

        else if (opcaoMenuPrincipal == "2")
            return new TelaRevista(repositorioCaixa, repositorioRevista);

        else if (opcaoMenuPrincipal == "3")
            return new TelaAmigo(repositorioAmigo);
        else if (opcaoMenuPrincipal == "4")
            return new TelaEmprestimo(repositorioEmprestimo, repositorioRevista, repositorioAmigo);
        else if (opcaoMenuPrincipal == "5")
            return new TelaMulta(RepositorioMulta);
        return null;
    }
}
