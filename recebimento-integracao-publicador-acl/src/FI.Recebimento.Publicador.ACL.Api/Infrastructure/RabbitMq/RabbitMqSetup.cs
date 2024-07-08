using MassTransit;
using System.Security.Authentication;
using FI.Recebimento.Publicador.ACL.Domain.AggregatesModel;
using FI.Recebimento.Publicador.ACL.Domain;

namespace FI.Recebimento.Publicador.ACL.Api.Infrastructure.RabbitMq;

[ExcludeFromCodeCoverage]
public static class RabbitMqSetup
{
    private const string SUFIXO_EXCHANGE_NAME = ".input_exchange";

    /// <summary>
    /// Resumo:
    /// Adiciona configuracoes de Consumer/Producer do MassTransit(RabbbitMq).
    /// </summary>
    /// <param name="services"></param>
    public static void AddRabbitMq(this IServiceCollection services)
    {
        services.AddMassTransit(masstransitConfig =>
        {
            var host = Constantes.RabbitMq.Host;
            var vhost = Constantes.RabbitMq.Vhost;
            var port = Constantes.RabbitMq.Port;
            var userName = Constantes.RabbitMq.UserName;
            var password = Constantes.RabbitMq.Password;
            var useSsl = Constantes.RabbitMq.UseSsl;
            var environment = Constantes.RabbitMq.Ambiente;

            services.AddHealthCheckRabbitMq(host, vhost, port, userName, password, useSsl);

            var hostname = $"rabbitmq://{host}:{Constantes.RabbitMq.Port}/{vhost}/";

            masstransitConfig.UsingRabbitMq((context, configRabbit) =>
            {
                // ExchangeName(s)
                ConfigureMessageEntityName<ExemploRabbit>(configRabbit, environment);


                configRabbit.Host(hostname, host =>
                {
                    host.Username(userName);
                    host.Password(password);

                    if (Constantes.RabbitMq.UseSsl)
                        host.UseSsl(ssl => ssl.Protocol = SslProtocols.Tls12);

                    host.ConfigureBatchPublish(batch => batch.Enabled = true);
                    //ToDo: Caso queria aguardar a confirmacao da entrega da mensagem colocar PublisherConfirmation = true. Ps: Isso pode onerar tempo de resposta.
                    host.PublisherConfirmation = false;
                });

                configRabbit.ClearSerialization();
                configRabbit.UseRawJsonSerializer();

                // Producer(s)
                configRabbit.Publish<ExemploRabbit>(configurator => ConfigurePublishEvent(configurator, configRabbit, nameof(ExemploRabbit), $"{environment}.queue.dominio.subdominio.tipodado.v1"));

                // Consumer(s)
            });
        });
    }

    /// <summary>
    /// Registra configuracoes referente ao consumer do RabbitMq
    /// <br></br>
    /// <br>Tipos esperados no exchangeType:</br>
    /// <br></br>
    /// <br><b>[<see cref="RabbitMQ.Client.ExchangeType.Direct"/>]
    /// <br><b>[<see cref="RabbitMQ.Client.ExchangeType.Fanout"/>]
    /// <br><b>[<see cref="RabbitMQ.Client.ExchangeType.Headers"/>]
    /// <br><b>[<see cref="RabbitMQ.Client.ExchangeType.Topic"/>]
    /// </summary>
    /// <param name="context"></param>
    /// <param name="configRabbit"></param>
    /// <param name="routingKey"></param>
    /// <param name="queueName"></param>
    /// <param name="retryLimit"></param>
    /// <param name="interval"></param>
    /// <param name="exchangeType"></param>
    /// <param name="configureConsumeTopology"></param>
    /// <param name="prefetchCount"></param>
    static void ConfigureEndpoint<TConsumer>(IBusRegistrationContext context,
                                             IRabbitMqBusFactoryConfigurator configRabbit,
                                             string routingKey,
                                             string queueName,
                                             int retryLimit = 2,
                                             TimeSpan interval = default,
                                             string exchangeType = RabbitMQ.Client.ExchangeType.Fanout,
                                             bool configureConsumeTopology = false,
                                             int prefetchCount = 2) where TConsumer : class, IConsumer
    {
        interval = interval == default ? TimeSpan.FromSeconds(3) : interval;

        configRabbit.ReceiveEndpoint(queueName, configureEndpoint =>
        {
            configureEndpoint.ConfigureConsumeTopology = configureConsumeTopology;
            configureEndpoint.PrefetchCount = prefetchCount;
            configureEndpoint.UseMessageRetry(retry =>
            {
                retry.Interval(retryLimit, interval); // Configuracoes de Retry. Observacao o index do retry inicia com 0.
                retry.Ignore<ConsumerCanceledException>();
                //ToDo: Tente novamente apos um atraso exponencialmente crescente, ate o limite de novas tentativas. Remover caso nao utilizar. Exemplo:  retry.Exponential(retryCount, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(3600), TimeSpan.FromSeconds(10)); // 5s(1); 50s(2); 500s(3); 5000s(4); 50000s(5)
                //ToDo: Estrategia de Exception customizada. Remover caso nao utilizar. Exemplo: retry.Ignore<StrategyException>();
            });

            configureEndpoint.ConfigureConsumer<TConsumer>(context);

            if (VerifyIsFanout(exchangeType))
            {
                configureEndpoint.Bind(GetQueueExchangeName(queueName), callback =>
                {
                    callback.ExchangeType = exchangeType;
                    callback.RoutingKey = routingKey;
                });
            }
        });
    }

    /// <summary>
    /// Registra configuracoes referente ao producer do RabbitMq
    /// <br></br>
    /// <br>Tipos esperados no exchangeType:</br>
    /// <br></br>
    /// <br><b>[<see cref="RabbitMQ.Client.ExchangeType.Direct"/>]
    /// <br><b>[<see cref="RabbitMQ.Client.ExchangeType.Fanout"/>]
    /// <br><b>[<see cref="RabbitMQ.Client.ExchangeType.Headers"/>]
    /// <br><b>[<see cref="RabbitMQ.Client.ExchangeType.Topic"/>]
    /// </summary>
    /// <param name="configRabbit"></param>
    /// <param name="factoryConfigurator"></param>
    /// <param name="routingKey"></param>
    /// <param name="queueName"></param>
    /// <param name="exchangeType"></param>
    static void ConfigurePublishEvent<T>(IRabbitMqMessagePublishTopologyConfigurator<T> configRabbit, 
        IRabbitMqBusFactoryConfigurator factoryConfigurator, 
        string routingKey,
        string queueName, 
        string exchangeType = RabbitMQ.Client.ExchangeType.Fanout) where T : class
    {
        var exchange = configRabbit.Exchange.ExchangeName;

        configRabbit.BindQueue(exchange, queueName, configure => {
            configure.ExchangeType = exchangeType;
            configure.RoutingKey = routingKey;
        });

        if (VerifyIsFanout(exchangeType))
        {
            factoryConfigurator.Publish<T>(configureTopology => configureTopology.ExchangeType = exchangeType);
            factoryConfigurator.MessageTopology.SetEntityNameFormatter(new PrefixEntityNameFormatter(factoryConfigurator.MessageTopology.EntityNameFormatter, GetQueueExchangeName(queueName)));
            factoryConfigurator.Send<T>(configureTopology =>
            {
                configureTopology.UseRoutingKeyFormatter(context => context.Message.GetType().Name);
            });
        }
    }

    /// <summary>
    /// Resumo:
    /// Concatena o ambiente corrente na aplicação ao nome da exchange para criação de filas distintas.
    /// </summary>
    /// <param name="configRabbit"></param>
    /// <param name="environment"></param>
    static void ConfigureMessageEntityName<T>(IRabbitMqBusFactoryConfigurator configRabbit, string environment) where T : class
        => configRabbit.Message<T>(type => { type.SetEntityName($"{typeof(T)}.{environment}");});

    static string GetQueueExchangeName(string queueName)
       => $"{queueName}{SUFIXO_EXCHANGE_NAME}";

    static string GetArgumentNullException(string argument)
        => $"Environment Variable {argument} not informed.";

    static bool VerifyIsFanout(string exchangeType)
        => !string.IsNullOrEmpty(exchangeType) && exchangeType != RabbitMQ.Client.ExchangeType.Fanout;
}
