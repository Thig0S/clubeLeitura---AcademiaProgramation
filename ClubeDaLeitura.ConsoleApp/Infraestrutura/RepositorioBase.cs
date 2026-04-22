using System;
using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public abstract class RepositorioBase
{
    protected EntidadeBase?[] registros = new EntidadeBase[100];

    public void Cadastrar(EntidadeBase entidade)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            if (registros[i] == null)
            {
                registros[i] = entidade;
                break;
            }
        }
    }

    public EntidadeBase?[] SelecionarTodas()
    {
        return registros;
    }

    public bool Editar(string idSelecionado, EntidadeBase entidade)
    {
        EntidadeBase? registroselecionada = SelecionarPorId(idSelecionado);

        if (registroselecionada == null)
            return false;

        registroselecionada.AtualizarRegistro(entidade);

        return true;
    }

    public bool Excluir(string idSelecionado)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            EntidadeBase? c = registros[i];

            if (c == null)
                continue;

            if (c.Id == idSelecionado)
            {
                registros[i] = null;
                return true;
            }
        }

        return false;
    }

    internal EntidadeBase SelecionarPorId(string idSelecionado)
    {
        EntidadeBase? registroselecionada = null;

        for (int i = 0; i < registros.Length; i++)
        {
            EntidadeBase? c = registros[i];

            if (c == null)
                continue;

            if (c.Id == idSelecionado)
            {
                registroselecionada = c;
                break;
            }
        }
        return registroselecionada;
    }
}