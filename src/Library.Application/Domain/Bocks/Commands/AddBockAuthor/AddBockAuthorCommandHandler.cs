using Library.Core.Common;
using Library.Core.Domain.Bocks.Common;
using Library.Core.Domain.Bocks.Data;
using Library.Core.Domain.Bocks.Models;
using MediatR;

namespace Library.Application.Domain.Bocks.Commands.AddBockAuthor;

public class AddBockAuthorCommandHandler(
    IAuthorMustExistChecker authorMustExistChecker,
    IBockMustExistChecker bockMustExistChecker,
    IBocksAuthorsRepository bockAuthorRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AddBockAuthorCommand>
{
    public async Task Handle(AddBockAuthorCommand command, CancellationToken cancellationToken)
    {
        var data = new CreateBockAuthorData(command.BockId, command.AuthorId);
        var bockAuthor = await BockAuthor.Create(
            authorMustExistChecker,
            bockMustExistChecker,
            data);
        bockAuthorRepository.Add(bockAuthor);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}