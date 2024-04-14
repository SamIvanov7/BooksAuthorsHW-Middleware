namespace Library.Api.Domain.Authors.Requests;

public record UpdateAuthorRequest(Guid Id, string FirstName, string LastName, string? MiddleName = default);