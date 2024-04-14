using MediatR;

namespace Library.Application.Domain.Bocks.Commands.DeleteBock;

public record DeleteBockCommand(Guid Id) : IRequest;