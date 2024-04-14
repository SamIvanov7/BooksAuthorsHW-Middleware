using Library.Core.Common;
using Library.Core.Domain.Authors.Common;
using Library.Core.Domain.Authors.Data;
using MediatR;

namespace Library.Application.Domain.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler(
    IAuthorsRepository authorsRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateAuthorCommand>
{
    public async Task Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = await authorsRepository.FindAsync(command.Id, cancellationToken);

        var data = new UpdateAuthorData(
            command.FirstName, 
            command.LastName, 
            command.MiddleName);

        author.Update(data);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}