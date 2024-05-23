using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace FI.Recebimento.Publicador.ACL.Api.Commands;

public class PublicarExemploRabbitCommand : IRequest
{
    public PublicarExemploRabbitCommand(string nome)
    {
        Nome = nome;
    }

    [Required]
    public string Nome { get; private set; }
}
