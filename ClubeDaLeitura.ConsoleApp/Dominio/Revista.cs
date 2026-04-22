

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public class Revista : EntidadeBase
{
    public string Titulo { get; set; }
    public int NumeroEdicao { get; set; }
    public int AnoPublicacao { get; set; }
    public Caixa Caixa { get; set; }

    public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa)
    {
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
        AnoPublicacao = anoPublicacao;
        Caixa = caixa;
    }

    public override string?[] Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrWhiteSpace(Titulo))
            erros += "O Campo Titulo é obrigadotorio;";

        else if (Titulo.Length < 2 || Titulo.Length > 100)
            erros += "O campo titulo deve conter entre 2 e 100 caracteres;";

        if (NumeroEdicao < 0)
            erros += "O campo numero da edicao deve ser maior que 0;";

        int anoAtual = DateTime.Now.Year;

        if (AnoPublicacao < 1 || AnoPublicacao > anoAtual)
            erros += "O campo ano publicacao deve conter uma data valida;";

        if (Caixa == null)
            erros += "O campo Caixa deve conter uma caixa valida;";

        return erros.Split(";", StringSplitOptions.RemoveEmptyEntries);


    }

    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        Revista novaRevista = (Revista)entidadeAtualizada;

        Titulo = novaRevista.Titulo;
        NumeroEdicao = novaRevista.NumeroEdicao;
        AnoPublicacao = novaRevista.AnoPublicacao;
        Caixa = novaRevista.Caixa;
    }
}
