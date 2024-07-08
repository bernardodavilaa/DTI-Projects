namespace FI.Recebimento.Publicador.ACL.Api.Middlewares;

[ExcludeFromCodeCoverage]
public class ExceptionMiddleware
{
    const string DEFAULT_EXCEPTION = "Ocorreu um erro inesperado.";
    const string CANCELED_EXCEPTION = "A solicitacao foi cancelada.";

    readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger logger)
    {
        try
        {
            context.Request.EnableBuffering();
            await _next(context);
        }
        catch (Exception exception)
        {
            switch (exception)
            {
                case OperationCanceledException:
                    await HandlingExceptionAsync(logger, context, StatusCodes.Status400BadRequest, CANCELED_EXCEPTION);
                    break;
                default:
                    await HandlingExceptionAsync(logger, context, StatusCodes.Status500InternalServerError, DEFAULT_EXCEPTION, exception);
                    break;
            }
        }
    }

    private static Task HandlingExceptionAsync(ILogger logger,
        HttpContext context,
        int statusCodes,
        string error,
        Exception? exception = default)
    {
        logger.Log(LogLevel.Debug, "Encountered a issue. Exception: {exception}",
            new List<ParametersLogModel>
                {
                    new (exception)
                },
                flow: nameof(ExceptionMiddleware));

        context.Response.StatusCode = statusCodes;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsJsonAsync(new ErrorModel(new ErrorModel.ErrorDetails()
        {
            Message = error,
            StatusCode = statusCodes
        }));
    }
}
