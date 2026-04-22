using System.Security.Cryptography;

namespace ClubeDaLeitura.ConsoleApp.Dominio;
public class Caixa
{
    public string Id { get; set; } = string.Empty; // propriedade
    public string Etiqueta { get; set; } = string.Empty; // propriedade
    public string Cor { get; set; } = string.Empty; // propriedade
    public int DiasDeEmprestimo { get; set; } = 7;  // propriedade

    // construtor de classe
    // toda instância que for criada PRECISA dessas informações
    public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
    {
        Id = Convert
                .ToHexString(RandomNumberGenerator.GetBytes(20))
                .ToLower()
                .Substring(0, 7);

        Etiqueta = etiqueta;
        Cor = cor;
        DiasDeEmprestimo = diasDeEmprestimo;
    }

    public string[] Validar()
    {
        string erros = string.Empty;

        if (String.IsNullOrWhiteSpace(Etiqueta))
        {
            erros += "O campo Etiqueta é obrigatorio!; ";
        }
        else if (Etiqueta.Length > 50)
        {
            erros += "Etiqueta deve ter no máximo 50 caracteres!; ";
        }
        if (DiasDeEmprestimo < 1)
        {
            erros += "O campo de dias de emprestimo deve conter um valor maior que 0;";
        }
        return erros.Split(";", StringSplitOptions.RemoveEmptyEntries);
    }

    public void AtualizarRegistro(Caixa caixaAtualizada)
    {
        Etiqueta = caixaAtualizada.Etiqueta;
        Cor = caixaAtualizada.Cor;
        DiasDeEmprestimo = caixaAtualizada.DiasDeEmprestimo;
    }
}
