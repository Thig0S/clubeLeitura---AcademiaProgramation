using System;
using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaEmprestimo
{
    public void Abrir()
    {
        //logica de abertura de cadastro de emprestimo

        //1. obter os dados obrigatorios revista e amigo
        Emprestimo emprestimo = ObterDadosCadastrais();
        //2. validar o emprestimo
        string[] erros = emprestimo.Validar();


        if (erros.Length > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (string s in erros)
            {
                System.Console.WriteLine(s);
            }
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
            Console.ResetColor();

            Abrir();
            return;
        }

        //3. armazenar essa porra     
    }

    private Emprestimo ObterDadosCadastrais()
    {
        throw new NotImplementedException();
    }
}
