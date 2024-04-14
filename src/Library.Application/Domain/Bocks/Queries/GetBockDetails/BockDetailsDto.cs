namespace Library.Application.Domain.Bocks.Queries.GetBockDetails;

public record BockDetailsDto
{
    public Guid Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public AuthorInformationDto[] Authors { get; init; }
}