using System;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura.Base;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioMulta : RepositorioBase
{
    private RepositorioEmprestimo RepositorioEmprestimo;

    public RepositorioMulta(RepositorioEmprestimo repositorioEmprestimo)
    {
        RepositorioEmprestimo = repositorioEmprestimo;
    }

    public void CadastrarMultas()
    {
        for (int i = 0; i < registros.Length; i++)
        {
            //pega toda a lista de emprestimos e add as multas.

            Emprestimo?[] emprestimos = RepositorioEmprestimo.SelecionarTodas();

            if (emprestimos[i] == null)
                continue;

            if (emprestimos[i].GerarMultaSeNecessario())

                registros[i] = emprestimos[i].Multa;
        }
    }

    internal bool VerificarMultaAmigo(string nome)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            Multa? multa = (Multa?)registros[i];

            if (multa == null)
                continue;

            if (multa.emprestimo.Amigo.Nome == nome && multa.Status == StatusMulta.Pendente)
            {
                return false;
            }
        }
        return true;
    }

    internal EntidadeBase?[] VisualizarTodos()
    {
        return registros;
    }
}
