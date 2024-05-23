using System;

using System.Diagnostics.CodeAnalysis;

namespace FI.Recebimento.Publicador.ACL.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class EnumExtensions
{
    public static TEnum TryParseEnum<TEnum>(this string? value) where TEnum : struct, Enum
    {
        if(Enum.TryParse(value, out TEnum result))
            return result;
        else
            throw new ArgumentException($"O valor ({value}) informado e invalido para o tipo {typeof(TEnum)}");
    }
}