using MediatR;
using Library.Core.Common;
using Library.Core.Domain.Bocks.Common;
using Library.Core.Domain.Bocks.Data;

namespace Library.Application.Domain.Bocks.Commands.UpdateBock;

public class UpdateBockCommandHandler(
    IBocksRepository bocksRepository, 
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateBockCommand>
{
    public async Task Handle(UpdateBockCommand command, CancellationToken cancellationToken)
    {
        var bock = await bocksRepository.FindAsync(command.Id, cancellationToken);
        var data = new UpdateBockData(command.Title, command.Description);
        bock.Update(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}