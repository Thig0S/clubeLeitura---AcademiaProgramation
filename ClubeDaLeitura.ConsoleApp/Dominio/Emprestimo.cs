using System;
using System.Security.Cryptography;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public class Emprestimo
{
    public string Id { get; set; } = string.Empty; // propriedade
    public Revista Revista { get; set; }
    public Amigo Amigo { get; set; }
    public DateTime Abertura { get; set; }
    public DateTime ConclusaoPrevista
    {
        get
        {
            int diasDeEmprestimo = Revista.Caixa.DiasDeEmprestimo;

            DateTime Conclusao = Abertura.AddDays(diasDeEmprestimo);

            return Conclusao;
        }
    }
    public bool EstaAtrasado
    {
        get
        {
            return Status == StatusEmprestimo.Aberto && DateTime.Now > ConclusaoPrevista;
        }
    }
    public StatusEmprestimo Status { get; set; } = StatusEmprestimo.Indefinido;
    public Emprestimo(Revista revista, Amigo amigo)
    {
        Id = Convert
                .ToHexString(RandomNumberGenerator.GetBytes(20))
                .ToLower()
                .Substring(0, 7);


        Revista = revista;
        Amigo = amigo;
    }

    public string[] Validar()
    {
        string erros = string.Empty;

        if (Revista == null)
            erros += "O campo Revista deve ser preenchido!;";

        if (Amigo == null)
            erros += "O campo Amigo deve ser preenchido!;";

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }

    public void Abrir()
    {
        Abertura = DateTime.Now;

        Status = StatusEmprestimo.Aberto;
        Revista.Emprestar();

        Amigo.AdicionarEmprestimo(this);
    }

    public void Concluir()
    {
        Revista.Devolver();
        Status = StatusEmprestimo.Concluido;
    }
}
