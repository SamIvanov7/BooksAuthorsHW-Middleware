using Library.Core.Common;
using Library.Core.Domain.Bocks.Common;
using MediatR;

namespace Library.Application.Domain.Bocks.Commands.DeleteBock;

public class DeleteBockCommandHandler(
    IBocksRepository bocksRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteBockCommand>
{
    public async Task Handle(DeleteBockCommand command, CancellationToken cancellationToken)
    {
        var bock = await bocksRepository.FindAsync(command.Id, cancellationToken);
        bocksRepository.Delete(new[] { bock });
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}