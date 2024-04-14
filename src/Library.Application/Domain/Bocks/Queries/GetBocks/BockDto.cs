namespace Library.Application.Domain.Bocks.Queries.GetBocks;

public record BockDto
{
    public Guid Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }
}