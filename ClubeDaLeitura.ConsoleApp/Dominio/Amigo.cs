using System;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public class Amigo : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string NomeResponsavel { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public Emprestimo[] Emprestimos { get; set; } = new Emprestimo[100];
    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        Nome = nome;
        NomeResponsavel = nomeResponsavel;
        Telefone = telefone;
    }
    public override string[] Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrEmpty(Nome))
            erros += "O campo \"Nome\" é obrigatório;";
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros += "O campo \"Nome\" deve conter entre 3 e 100 caracteres;";

        if (string.IsNullOrEmpty(NomeResponsavel))
            erros += "O campo \"Nome do Responsável\" é obrigatório;";

        if (string.IsNullOrEmpty(Telefone))
            erros += "O campo \"Telefone\" é obrigatório;";
        int contadorDigitos = 0;

        string telefoneEncurtado = Telefone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
        bool contemLetraOuSimbolo = false;

        for (int i = 0; i < telefoneEncurtado.Length; i++)
        {
            char c = telefoneEncurtado[i];
            if (char.IsDigit(c))
                contadorDigitos++;
            else
            {
                contemLetraOuSimbolo = true;
                break;
            }
        }

        if (contadorDigitos < 10 || contadorDigitos > 11)
            erros += "O campo \"Telefone\" deve conter entre 10 e 11 dígitos;";

        if (contemLetraOuSimbolo)
            erros += "O campo \"Telefone\" deve conter apenas dígitos;";

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }
    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        Amigo amigoAtualizado = (Amigo)entidadeAtualizada;

        Nome = amigoAtualizado.Nome;
        NomeResponsavel = amigoAtualizado.NomeResponsavel;
        Telefone = amigoAtualizado.Telefone;
    }

    internal void AdicionarEmprestimo(Emprestimo emprestimo)
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
}
