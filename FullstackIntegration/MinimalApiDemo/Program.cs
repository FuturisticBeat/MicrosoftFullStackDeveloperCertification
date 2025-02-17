WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// In-memory list to store tasks
List<TaskItem> tasks = [];

// GET all tasks
app.MapGet("/tasks", () => TypedResults.Ok(tasks));

// POST a new task
app.MapPost("/tasks", (TaskItem task) =>
{
    task.Id = tasks.Count + 1;
    tasks.Add(task);
    return TypedResults.Created($"/tasks/{task.Id}", task);
});

// UPDATE a task
app.MapPut("tasks/{id:int}", IResult (int id, TaskItem updatedTask) =>
{
    TaskItem? existingTask = tasks.FirstOrDefault(t => t.Id == id);
    if (existingTask == null)
    {
        return TypedResults.NotFound();
    }

    existingTask.Title = updatedTask.Title;
    existingTask.IsCompleted = updatedTask.IsCompleted;
    return TypedResults.Ok(existingTask);
});

// DELETE a task
app.MapDelete("tasks/{id:int}", IResult (int id) =>
{
    TaskItem? existingTask = tasks.FirstOrDefault(t => t.Id == id);
    if (existingTask == null)
    {
        return TypedResults.NotFound();
    }

    tasks.Remove(existingTask);
    return TypedResults.Ok(existingTask);
});

app.Run();

public class TaskItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public bool IsCompleted { get; set; }
}