var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/* static routes */

app.MapGet("/", () => "Hello World!");

/* dynamic routes */

// route parameters
app.MapGet("/users/{userId}/posts/{slug}", (int userId, string slug) => $"Hello user {userId} - {slug}");

// route constraint
app.MapGet("/products/{id:int:min(0)}", (int id) => $"Product id: {id}");

// optional route parameters
app.MapGet("/report/{year?}", (int? year = 2016) => $"Report for year {year}");

// catch-all route parameters
app.MapGet("/files/{*path}", (string path) => $"File path: {path}");

// query parameters
app.MapGet("/search", (string? q, int page = 1) => $"Searching for: {q} on page {page}");

app.Run();
