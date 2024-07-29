namespace CSharpApp.Core.Dtos;

public record CommentRecord(
    [property: JsonProperty("postId")] int PostId,
    [property: JsonProperty("id")] int Id,
    [property: JsonProperty("name")] string Name,
    [property: JsonProperty("email")] string Email,
    [property: JsonProperty("body")] string Body
);