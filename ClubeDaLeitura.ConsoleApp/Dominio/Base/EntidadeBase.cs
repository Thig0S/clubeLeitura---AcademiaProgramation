using System;
using System.Security.Cryptography;

namespace ClubeDaLeitura.ConsoleApp.Dominio.Base;

public abstract class EntidadeBase
{
    public string Id { get; set; } = string.Empty; // propriedade

    public EntidadeBase()
    {
        Id = Convert
                .ToHexString(RandomNumberGenerator.GetBytes(20))
                .ToLower()
                .Substring(0, 7);
    }

    public abstract void AtualizarRegistro(EntidadeBase entidadeAtualizada);
    public abstract string[] Validar();


}
