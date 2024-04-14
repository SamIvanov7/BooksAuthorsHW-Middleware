using Library.Core.Common;
using Library.Core.Domain.Authors.Models;
using Library.Core.Domain.Bocks.Common;
using Library.Core.Domain.Bocks.Data;
using Library.Core.Domain.Bocks.Validators;

namespace Library.Core.Domain.Bocks.Models;

public class BockAuthor : Entity
{
    private BockAuthor()
    {
    }

    internal BockAuthor(Guid bockId, Guid authorId)
    {
        BockId = bockId;
        AuthorId = authorId;
    }

    public Guid BockId { get; set; }

    public Bock Bock { get; set; }

    public Guid AuthorId { get; set; }

    public Author Author { get; set; }

    public static async Task<BockAuthor> Create(
        IAuthorMustExistChecker authorMustExistChecker,
        IBockMustExistChecker bockMustExistChecker,
        CreateBockAuthorData data)
    {
        // validate
        await ValidateAsync(new CreateBockAuthorValidator(authorMustExistChecker, bockMustExistChecker), data);

        // create
        return new BockAuthor(data.BockId, data.AuthorId);
    }
}