using System.Diagnostics.CodeAnalysis;

namespace FI.Recebimento.Publicador.ACL;

/// <summary>
/// Error Model
/// </summary>
[ExcludeFromCodeCoverage]
public class ErrorModel
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }

    public List<ErrorDetails> Erros { get; } = new List<ErrorDetails>();

    public ErrorModel(ErrorDetails erro)
    {
        Erros.Add(erro);
    }

    public ErrorModel(IEnumerable<ErrorDetails> erros)
    {
        Erros.AddRange(erros);
    }
}