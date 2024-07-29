using CSharpApp.Core.Dtos;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddDefaultConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Todos via TodoService.
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

// Posts via PostService.
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
    .WithName("GetPostById")
    .WithOpenApi();

app.MapPost("/posts", async (PostRecord postRecord, IPostService postService) =>
{
    var post = await postService.InsertRecord(postRecord);
    return post;
})
    .WithName("InsertPost")
    .WithOpenApi();


app.MapPut("/posts", async (PostRecord postRecord, IPostService postService) =>
{
    var post = await postService.UpdateRecord(postRecord);
    return post;
})
    .WithName("UpdatePost")
    .WithOpenApi();


app.MapDelete("/posts/{id}", async ([FromRoute] int id, IPostService postService) =>
{
    var post = await postService.DeleteRecord(id);
    return post;
})
    .WithName("DeletePost")
    .WithOpenApi();


// Comments via generic wrapper.
app.MapGet("/comments", async (IHttpClientWrapper httpClientWrapper) =>
{
    var comments = await httpClientWrapper.GetAllRecords<CommentRecord>("/comments");
    return comments;
})
    .WithName("GetComments")
    .WithOpenApi();

app.MapGet("/comments/{id}", async ([FromRoute] int id, IHttpClientWrapper httpClientWrapper) =>
{
    var comment = await httpClientWrapper.GetRecordById<CommentRecord>($"/comments/{id}");
    return comment;
})
    .WithName("GetCommentById")
    .WithOpenApi();

app.MapPost("/comments", async (CommentRecord postRecord, IHttpClientWrapper httpClientWrapper) =>
{
    var comment = await httpClientWrapper.InsertRecord("/comments", postRecord);
    return comment;
})
    .WithName("InsertComment")
    .WithOpenApi();


app.MapPut("/comments", async (PostRecord postRecord, IHttpClientWrapper httpClientWrapper) =>
{
    var comment = await httpClientWrapper.UpdateRecord("/comments", postRecord);
    return comment;
})
    .WithName("UpdateComment")
    .WithOpenApi();


app.MapDelete("/comments/{id}", async ([FromRoute] int id, IHttpClientWrapper httpClientWrapper) =>
{
    var comment = await httpClientWrapper.DeleteRecord<CommentRecord>($"/comments/{id}");
    return comment;
})
    .WithName("DeleteComment")
    .WithOpenApi();

app.Run();