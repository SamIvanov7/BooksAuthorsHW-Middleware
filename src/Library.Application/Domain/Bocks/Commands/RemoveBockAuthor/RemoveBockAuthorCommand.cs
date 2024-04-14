using MediatR;

namespace Library.Application.Domain.Bocks.Commands.RemoveBockAuthor;

public record RemoveBockAuthorCommand(Guid BockId, Guid AuthorId) : IRequest;