using System;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public abstract class TelaBase
{
    private string NomeEntidade = string.Empty;

    protected TelaBase(string nomeEntidade)
    {
        NomeEntidade = nomeEntidade;
    }

    protected void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {NomeEntidade}");
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

    public string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {NomeEntidade}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastrar {NomeEntidade}");
        Console.WriteLine($"2 - Editar {NomeEntidade}");
        Console.WriteLine($"3 - Excluir {NomeEntidade}");
        Console.WriteLine($"4 - Visualizar {NomeEntidade}s");
        Console.WriteLine($"S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }
}
