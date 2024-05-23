using BuildingBlock.CorrelationId.ServiceCollectionExtensions;
using System.Reflection;
using FI.Recebimento.Publicador.ACL.Api.Extensions;
using Localiza.BuildingBlocks.HealthChecks.Configuration;
using FI.Recebimento.Publicador.ACL.Infrastructure;
using FI.Recebimento.Publicador.ACL.Api.Middlewares;
const string AMBIENTE_PRODUCAO = "prd";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorrelationIdGenerator();

builder.Services
    .AddLocalizaLabsLogging()
    //ToDo: Validar se o LogLevel esta como LogLevel.Information e alterar para FI.Recebimento.Publicador.ACL.Api.Extensions.EnumExtensions.TryParseEnum<Localiza.BuildingBlocks.Logging.Enums.LogLevel>(Environment.GetEnvironmentVariable("LOG_LEVEL"))
    .SetLogLevel(EnumExtensions.TryParseEnum<LogLevel>(Environment.GetEnvironmentVariable("LOG_LEVEL")))
    .UseObfuscateSensiteData()
    .WithConsoleOutput()
    .WithConsoleOutputJsonFormat();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(LoggingActionFilter));
});
builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault);
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "recebimento-integracao-publicador-acl", Description = "API genérica para criação de ACL de descida da SAP", Version = "v1" });
});

builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;
});

builder.Services.AddInfrastructure();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
HttpContextAccessorProvider.Configure(app.Services.GetService<IHttpContextAccessor>());

app.UsePathBase("/recebimento-integracao-publicador-acl");

if (Environment.GetEnvironmentVariable("AMBIENTE") != AMBIENTE_PRODUCAO)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/recebimento-integracao-publicador-acl/swagger/v1/swagger.json", "recebimento-integracao-publicador-acl v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseResponseCompression();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health");
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Run();

// ToDo: Validar se o build foi executado com sucesso
// ToDo: Publicar a API no ambiente de desenvolvimento
// ToDo: Excluir a classe de teste WeatherForecast.cs
// ToDo: Configurar o API Gateway

//https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/error-handling?view=aspnetcore-7.0