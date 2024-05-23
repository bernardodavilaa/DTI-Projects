using System;
using System.Diagnostics.CodeAnalysis;

namespace FI.Recebimento.Publicador.ACL.Domain.AggregatesModel
{
    [ExcludeFromCodeCoverage]
    
    /// <summary>
    /// Classe que representa a entidade ExemploRabbit.
    /// </summary>
    public class ExemploRabbit
    {
        /// <summary>
        /// Inicializa uma nova instância da classe ExemploRabbit com o identificador e o valor do atributo especificados.
        /// </summary>
        /// <param name="id">O identificador único da entidade.</param>
        /// <param name="nome">O valor do atributo Nome.</param>
        public ExemploRabbit(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    
        /// <summary>
        /// Obtém o identificador único da entidade ExemploRabbit.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Obtém ou define o valor do atributo Nome da entidade ExemploRabbit.
        /// </summary>
        public string? Nome { get; private set; }
    }
}
