using MediatR;

namespace Library.Application.Domain.Bocks.Commands.CreateBock;

public record CreateBockCommand(string Title, string Description) : IRequest<Guid>;