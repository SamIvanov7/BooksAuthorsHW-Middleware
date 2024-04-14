using Library.Core.Common;
using Library.Core.Domain.Bocks.Common;
using MediatR;

namespace Library.Application.Domain.Bocks.Commands.RemoveBockAuthor;

public class RemoveBockAuthorCommandHandler(
    IBocksAuthorsRepository bockAuthorRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RemoveBockAuthorCommand>
{
    public async Task Handle(RemoveBockAuthorCommand command, CancellationToken cancellationToken)
    {
        var bockAuthor = await bockAuthorRepository.FindBockAuthorAsync(command.BockId, command.AuthorId, cancellationToken);
        bockAuthorRepository.Delete(bockAuthor);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}