using System;
using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioBase
{
    public bool Editar(string idSelecionado, EntidadeBase novaEntidade)
    {
        EntidadeBase? entidadeSelecionada = SelecionarPorId(idSelecionado);

        if (entidadeSelecionada == null)
            return false;

        entidadeSelecionada.AtualizarRegistro(novaEntidade);

        return true;
    }

    internal EntidadeBase SelecionarPorId(string idSelecionado)
    {
        return null;

    }
}