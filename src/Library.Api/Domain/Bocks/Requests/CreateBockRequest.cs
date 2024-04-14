namespace Library.Api.Domain.Bocks.Requests;

public record CreateBockRequest
{
    public string Title { get; init; }

    public string Description { get; init; }
}