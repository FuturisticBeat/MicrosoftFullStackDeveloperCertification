using StoreApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseMiddleware<GlobalLoggingAndExceptionHandlingMiddleware>();

app.UseHttpLogging();

app.MapControllers();

app.Run();
