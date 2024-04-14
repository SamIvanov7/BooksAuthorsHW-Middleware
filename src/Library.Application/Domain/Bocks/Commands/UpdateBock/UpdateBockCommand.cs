using MediatR;

namespace Library.Application.Domain.Bocks.Commands.UpdateBock;

public record UpdateBockCommand(Guid Id, string Title, string Description) : IRequest;