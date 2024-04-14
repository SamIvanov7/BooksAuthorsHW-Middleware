using Library.Core.Common;
using Library.Core.Domain.Authors.Data;
using Library.Core.Domain.Authors.Validators;
using Library.Core.Domain.Bocks.Models;

namespace Library.Core.Domain.Authors.Models;

public class Author : Entity, IAggregateRoot
{
    private readonly List<BockAuthor> _bocksAuthors = new();
    
    private Author()
    {
    }

    public Guid Id { get; private set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? MiddleName { get; set; }

    public IReadOnlyCollection<BockAuthor> BocksAuthors => _bocksAuthors.AsReadOnly();

    public static Author Create(CreateAuthorData data)
    {
        // validate
        Validate(new CreateAuthorValidator(), data);

        return new Author
        {
            Id = Guid.NewGuid(),
            FirstName = data.FirstName,
            LastName = data.LastName,
            MiddleName = data.MiddleName
        };
    }

    public void Update(UpdateAuthorData data)
    {
        // validate
        Validate(new UpdateAuthorValidator(), data);

        FirstName = data.FirstName;
        LastName = data.LastName;
        MiddleName = data.MiddleName;
    }
}