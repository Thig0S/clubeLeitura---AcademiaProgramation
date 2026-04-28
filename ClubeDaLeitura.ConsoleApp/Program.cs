
using ClubeDaLeitura.ConsoleApp.Apresentacao;
using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
RepositorioRevista repositorioRevista = new RepositorioRevista();
RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
RepositorioMulta repositorioMulta = new RepositorioMulta(repositorioEmprestimo);


TelaPrincipal telaPrincipal = new(repositorioCaixa, repositorioRevista, repositorioAmigo, repositorioEmprestimo, repositorioMulta);

while (true)
{
    ITela? telaSelecionada = telaPrincipal.ApresentarMenuOpcoesPrincipal();

    if (telaSelecionada == null)
        break;

    while (true)
    {
        string? opcaoMenuInterno = telaSelecionada.ObterOpcaoMenu();

        if (opcaoMenuInterno == "S")
            break;

        if (telaSelecionada is TelaBase)
        {
            TelaBase telaBase = (TelaBase)telaSelecionada;

            if (opcaoMenuInterno == "1")
                telaBase.Cadastrar();

            else if (opcaoMenuInterno == "2")
                telaBase.Editar();

            else if (opcaoMenuInterno == "3")
                telaBase.Excluir();

            else if (opcaoMenuInterno == "4")
                telaBase.VisualizarTodos(deveExibirCabecalho: true);
        }

        if (telaSelecionada is TelaEmprestimo)
        {
            TelaEmprestimo telaEmprestimo = (TelaEmprestimo)telaSelecionada;

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }
            if (opcaoMenuInterno == "1")
                telaEmprestimo.Abrir();

            else if (opcaoMenuInterno == "2")
                telaEmprestimo.Concluir();

            else if (opcaoMenuInterno == "3")
                telaEmprestimo.VisualizarTodos(deveExibirCabecalho: true);
        }
        if (telaSelecionada is TelaMulta)
        {
            TelaMulta telaMulta = (TelaMulta)telaSelecionada;

            if (opcaoMenuInterno == "S")
            {
                // Console.Clear();
                break;
            }
            if (opcaoMenuInterno == "1")
                telaMulta.VisualizarTodos(deveExibirCabecalho: true);

            else if (opcaoMenuInterno == "2")
                telaMulta.Quitar();

            else if (opcaoMenuInterno == "3")
                telaMulta.VisualizarAmigo(deveExibirCabecalho: true);
        }
    }
}