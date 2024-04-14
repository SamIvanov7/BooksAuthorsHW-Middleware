using System.ComponentModel.DataAnnotations;

namespace Library.Application.Domain.Authors.Queries.GetAuthors;

public record AuthorDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string FirstName { get; init; }

    [Required]
    public string LastName { get; init; }

    public string? MiddleName { get; init; }
}