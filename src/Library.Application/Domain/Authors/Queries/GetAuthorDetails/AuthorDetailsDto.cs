using System.ComponentModel.DataAnnotations;

namespace Library.Application.Domain.Authors.Queries.GetAuthorDetails;

public record AuthorDetailsDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string FirstName { get; init; }

    [Required]
    public string LastName { get; init; }

    public string? MiddleName { get; init; }

    public List<BockDto> Bocks { get; init; }
}