namespace Library.Application.Domain.Bocks.Queries.GetBockDetails;

public record AuthorInformationDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}