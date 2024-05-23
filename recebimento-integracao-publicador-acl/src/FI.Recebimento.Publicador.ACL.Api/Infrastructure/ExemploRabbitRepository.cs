using System.Linq;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Diagnostics.CodeAnalysis;
using FI.Recebimento.Publicador.ACL.Domain.AggregatesModel;
using FI.Recebimento.Publicador.ACL.Domain;
using FI.Recebimento.Publicador.ACL.Infrastructure;

namespace FI.Recebimento.Publicador.ACL.Infrastructure;

/// <summary>
/// Classe que implementa o repositório de ExemploRabbit.
/// </summary>
[ExcludeFromCodeCoverage]
public class ExemploRabbitRepository : IExemploRabbitRepository
{
    private static readonly List<ExemploRabbit> exemplorabbits = new();

    
}