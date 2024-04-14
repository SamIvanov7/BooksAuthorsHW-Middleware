using MediatR;

namespace Library.Application.Domain.Authors.Commands.DeleteAuthor;

public record DeleteAuthorCommand(IReadOnlyCollection<Guid> Ids) : IRequest;