using Backend.Data;
using Microsoft.EntityFrameworkCore;
using SharedModels;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS to allow requests from http://localhost
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .WithOrigins("http://localhost:5037")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Add the ProductsDbContext to the service container and configure it to use SQL Server
// The connection string is retrieved from the appsettings.Development.json file
// Ensure that the SQL Server instance is running and accessible
builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// Retrieves all products from the database.
app.MapGet("/products", async (ProductsDbContext db) => await db.Products.ToListAsync());

// Retrieves a product by its ID from the database.
app.MapGet("/products/{id:int}", async Task<IResult> (int id, ProductsDbContext db) =>
    await db.Products.FindAsync(id) is { } product ? 
        TypedResults.Ok(product) : 
        TypedResults.NotFound());

// Adds a new product to the database. The product data is sent in the request body.
app.MapPost("/products", async (Product product, ProductsDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/products/{product.ProductId}", product);
});

app.Run();