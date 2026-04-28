
using System.Security.Cryptography;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public enum StatusMulta
{
    Pendente,
    Quitado
}
public class Multa : EntidadeBase
{
    public Emprestimo emprestimo;
    public StatusMulta Status = StatusMulta.Pendente;
    public int Valor
    {
        get
        {
            return CalcularMulta();
        }
    }

    public Multa(Emprestimo emprestimo) : base()
    {
        this.emprestimo = emprestimo;
    }

    public int CalcularMulta()
    {
        int diasEmAberto = (emprestimo.ConclusaoPrevista - emprestimo.Abertura).Days;
        int valorMulta = 2 * diasEmAberto;

        return valorMulta;
    }
    public void PagarMulta()
    {
        Status = StatusMulta.Quitado;
    }

    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        throw new NotImplementedException();
    }

    public override string[] Validar()
    {
        throw new NotImplementedException();
    }
}
