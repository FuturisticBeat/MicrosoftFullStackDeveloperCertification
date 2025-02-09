var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

List<Blog> blogs = new List<Blog>()
{
    new Blog { Title = "Blog 1", Content = "Content 1" },
    new Blog { Title = "Blog 2", Content = "Content 2" },
    new Blog { Title = "Blog 3", Content = "Content 3" }
};

// anonymous function to check if the id is out of bounds
Func<int, bool> isOutOfBounds = (int id) => id < 0 || id >= blogs.Count;

// CREATE
app.MapPost("/blogs", (Blog blog) =>
{
    blogs.Add(blog);
    return Results.Created($"/blogs/{blogs.Count - 1}", blog);
});

// READ
app.MapGet("/blogs", () => blogs);

app.MapGet("/blogs/{id:int}", (int id) =>
{
    if (isOutOfBounds(id))
    {
        return Results.NotFound();
    }
    return Results.Ok(blogs[id]);
});

// UPDATE
app.MapPut("/blogs/{id:int}", (int id, Blog blog) =>
{
    if (isOutOfBounds(id))
    {
        return Results.NotFound();
    }
    blogs[id] = blog;
    return Results.Ok(blog);
});

// DELETE
app.MapDelete("/blogs/{id:int}", (int id) =>
{
    if (isOutOfBounds(id))
    {
        return Results.NotFound();
    }
    blogs.RemoveAt(id);
    return Results.NoContent();
});

app.Run();

public class Blog
{
    public required string Title { get; set; }
    public required string Content { get; set; }
}