namespace Library.Application.Domain.Authors.Queries.GetAuthorDetails;

public record BockDto
{
    public Guid Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }
}