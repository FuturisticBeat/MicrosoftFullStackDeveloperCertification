using System.Text.Json.Serialization;
using Backend.Data;
using Backend.Hubs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

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

// Add SignalR to the service container
builder.Services.AddSignalR();

// Add the ProductsDbContext to the service container and configure it to use SQL Server
// The connection string is retrieved from the appsettings.Development.json file
// Ensure that the SQL Server instance is running and accessible
builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure JSON serialization options to preserve object references.
// This is necessary to handle circular references in the object graph,
// which can occur when navigating relationships between entities.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
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
app.MapGet("/api/products", async (ProductsDbContext db) => await db.Products
    .Include(p => p.Category).ToListAsync());

// Retrieves a product by its ID from the database.
app.MapGet("/api/products/{id:int}", async Task<IResult> (int id, ProductsDbContext db) =>
    await db.Products.Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.ProductId == id) is { } product
        ? TypedResults.Ok(product)
        : TypedResults.NotFound());

// Adds a new product to the database. The product data is sent in the request body.
app.MapPost("/api/products", async (Product product, ProductsDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/products/{product.ProductId}", product);
});

// Updates an existing product in the database. The product data is sent in the request body.
app.MapPut("/api/products/{id:int}", async Task<IResult> (int id, Product product, ProductsDbContext db) =>
{
    Product? existingProduct = await db.Products.FindAsync(id);
    if (existingProduct == null)
    {
        return TypedResults.NotFound("Product not found");
    }

    existingProduct.Name = product.Name;
    existingProduct.Description = product.Description;
    existingProduct.Price = product.Price;
    existingProduct.Stock = product.Stock;

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
});

// Deletes a product by its ID from the database.
app.MapDelete("/api/products/{id:int}", async Task<IResult> (int id, ProductsDbContext db) =>
{
    Product? product = await db.Products.FindAsync(id);
    if (product == null)
    {
        return TypedResults.NotFound("Product not found");
    }

    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
});

// Maps the ChatHub to the /chat endpoint for SignalR communication.
app.MapHub<ChatHub>("/chat");

app.Run();