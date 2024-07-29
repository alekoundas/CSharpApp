using CSharpApp.Core.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddDefaultConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<HttpClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/todos", async (ITodoService todoService) =>
    {
        var todos = await todoService.GetAllTodos();
        return todos;
    })
    .WithName("GetTodos")
    .WithOpenApi();

app.MapGet("/todos/{id}", async ([FromRoute] int id, ITodoService todoService) =>
    {
        var todos = await todoService.GetTodoById(id);
        return todos;
    })
    .WithName("GetTodosById")
    .WithOpenApi();

app.MapGet("/posts", async (IPostService postService) =>
{
    var posts = await postService.GetAllRecords();
    return posts;
})
    .WithName("GetPosts")
    .WithOpenApi();

app.MapGet("/posts/{id}", async ([FromRoute] int id, IPostService postService) =>
{
    var post = await postService.GetByRecordId(id);
    return post;
})
    .WithName("GetPostsById")
    .WithOpenApi();


app.MapPost("/posts", async (PostRecord postRecord, IPostService postService) =>
{
    var post = await postService.InsertRecord(postRecord);
    return post;
})
    .WithName("InsertPost")
    .WithOpenApi();


app.MapDelete("/posts/{id}", async ([FromRoute] int id, IPostService postService) =>
{
    var post = await postService.DeleteRecord(id);
    return post;
})
    .WithName("DeletePost")
    .WithOpenApi();

app.Run();