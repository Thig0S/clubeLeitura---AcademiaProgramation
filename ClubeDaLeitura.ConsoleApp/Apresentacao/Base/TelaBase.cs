using System;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura.Base;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao.Base;

public abstract class TelaBase
{
    private string NomeEntidade = string.Empty;
    private RepositorioBase repositorio;
    protected TelaBase(string nomeEntidade, RepositorioBase repositorio)
    {
        NomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public void Cadastrar()
    {
        ExibirCabecalho($"Cadastro de {NomeEntidade}");

        EntidadeBase novaEntidade = ObterDadosCadastrais();

        string?[] erros = novaEntidade.Validar();


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
        Console.ResetColor();

        repositorio.Cadastrar(novaEntidade);

        ExibirMensagem($"O registro \"{novaEntidade.Id}\" foi cadastrado com sucesso!");
    }

    public void Editar()
    {
        ExibirCabecalho($"Edição de {NomeEntidade}");

        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja editar: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        Console.WriteLine("---------------------------------");

        EntidadeBase? novaEntidade = ObterDadosCadastrais();

        string[] erros = novaEntidade.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine("---------------------------------");

            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < erros.Length; i++)
            {
                string erro = erros[i];

                Console.WriteLine(erro);
            }

            Console.ResetColor();
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();

            // Recursão
            Editar();
            return;
        }
        Console.ResetColor();

        bool conseguiuEditar = repositorio.Editar(idSelecionado, novaEntidade);

        if (!conseguiuEditar)
        {
            ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }

        ExibirMensagem($"O registro \"{idSelecionado}\" foi editado com sucesso.");
    }

    public void Excluir()
    {
        ExibirCabecalho("Exclusão de Caixa");

        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja excluir: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        bool conseguiuExcluir = repositorio.Excluir(idSelecionado);

        if (!conseguiuExcluir)
        {
            ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }

        ExibirMensagem($"O registro \"{idSelecionado}\" foi excluído com sucesso.");
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

    protected abstract EntidadeBase ObterDadosCadastrais();

    public abstract void VisualizarTodos(bool deveExibirCabecalho);

}
