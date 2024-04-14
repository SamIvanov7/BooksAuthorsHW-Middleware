namespace Library.Api.Domain.Bocks.Requests;

public record UpdateBockRequest
{
    public Guid Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }
}