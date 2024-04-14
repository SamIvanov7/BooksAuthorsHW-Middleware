using Library.Core.Common;
using Library.Core.Domain.Authors.Common;
using MediatR;

namespace Library.Application.Domain.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler(
    IAuthorsRepository authorsRepository, 
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteAuthorCommand>
{
    public async Task Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
    {
        var authors = await authorsRepository.FindManyAsync(command.Ids, cancellationToken);

        authorsRepository.Delete(authors);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}