using System;
using System.Diagnostics.CodeAnalysis;

namespace FI.Recebimento.Publicador.ACL.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class Constantes
{
    public static class RabbitMq
    {
        public static string Host => Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? throw new ApplicationException(GetArgumentNullException("RABBITMQ_HOST"));
        public static string Vhost => Environment.GetEnvironmentVariable("RABBITMQ_VHOST") ?? throw new ApplicationException(GetArgumentNullException("RABBITMQ_VHOST"));
        public static string UserName => Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? throw new ApplicationException(GetArgumentNullException("RABBITMQ_USER"));
        public static string Password => Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? throw new ApplicationException(GetArgumentNullException("RABBITMQ_PASSWORD"));
        public static string Ambiente => Environment.GetEnvironmentVariable("AMBIENTE")?.ToLower() ?? throw new ApplicationException(GetArgumentNullException("AMBIENTE"));
        public static bool UseSsl
        {
            get
            {
                _ = bool.TryParse(Environment.GetEnvironmentVariable("RABBITMQ_USE_SSL"), out bool useSsl);
                return useSsl;
            }
        }
        public static ushort Port
        {
            get
            {
                _ = ushort.TryParse(Environment.GetEnvironmentVariable("RABBITMQ_PORT"), out ushort port);
                return port;
            }
        }
    }
    static string GetArgumentNullException(string argument)
        => $"Environment Variable {argument} not informed.";
}