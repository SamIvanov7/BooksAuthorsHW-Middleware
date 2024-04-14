using MediatR;

namespace Library.Application.Domain.Authors.Queries.GetAuthorDetails;

public record GetAuthorDetailsQuery(Guid Id) : IRequest<AuthorDetailsDto>;