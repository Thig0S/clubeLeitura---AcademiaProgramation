using System;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioEmprestimo
{
    private Emprestimo?[] Emprestimos = new Emprestimo[100];

    public void Cadastrar(Emprestimo emprestimo)
    {
        for (int i = 0; i < Emprestimos.Length; i++)
        {
            if (Emprestimos[i] == null)
            {
                Emprestimos[i] = emprestimo;
                break;
            }
        }
    }

    public Emprestimo?[] SelecionarTodas()
    {
        return Emprestimos;
    }

    public Emprestimo? SelecionarPorId(string idSelecionado)
    {
        {
            Emprestimo? registroselecionada = null;

            for (int i = 0; i < Emprestimos.Length; i++)
            {
                Emprestimo? c = Emprestimos[i];

                if (c == null)
                    continue;

                if (c.Id == idSelecionado)
                {
                    registroselecionada = Emprestimos[i];
                    return registroselecionada;
                }
            }
            return null;
        }
    }
}
