using BasicApiEndpoints.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

// GET
app.MapGet("/", () => "Root Path");
app.MapGet("/downloads", () => "downloads");

// PUT
app.MapPut("/", () => "Put Path");

// DELETE
app.MapDelete("/", () => "Delete Path");

// POST
app.MapPost("/", () => "Post Path");

app.Run();
