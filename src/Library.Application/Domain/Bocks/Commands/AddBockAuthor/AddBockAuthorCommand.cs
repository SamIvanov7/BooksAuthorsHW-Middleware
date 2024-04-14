using MediatR;

namespace Library.Application.Domain.Bocks.Commands.AddBockAuthor;

public record AddBockAuthorCommand(Guid BockId, Guid AuthorId) : IRequest;