namespace Library.Api.Domain.Authors.Requests;

public record CreateAuthorRequest(
    string FirstName, 
    string LastName, 
    string? MiddleName = default);