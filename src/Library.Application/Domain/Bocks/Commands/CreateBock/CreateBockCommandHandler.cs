using Library.Core.Common;
using Library.Core.Domain.Bocks.Common;
using Library.Core.Domain.Bocks.Data;
using Library.Core.Domain.Bocks.Models;
using MediatR;

namespace Library.Application.Domain.Bocks.Commands.CreateBock;

public class CreateBockCommandHandler(
    IBocksRepository bocksRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateBockCommand, Guid>
{
    public async Task<Guid> Handle(CreateBockCommand command, CancellationToken cancellationToken)
    {
        var data = new CreateBockData(command.Title, command.Description);
        var bock = Bock.Create(data);
        bocksRepository.Add(bock);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return bock.Id;
    }
}