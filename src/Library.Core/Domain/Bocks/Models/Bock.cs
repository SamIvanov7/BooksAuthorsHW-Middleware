using Library.Core.Common;
using Library.Core.Domain.Bocks.Data;
using Library.Core.Domain.Bocks.Validators;

namespace Library.Core.Domain.Bocks.Models;

public class Bock : Entity, IAggregateRoot
{
    private readonly List<BockAuthor> _bocksAuthors = new ();

    private Bock()
    {
    }

    internal Bock(Guid id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    public Guid Id { get; private set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IReadOnlyCollection<BockAuthor> BocksAuthors => _bocksAuthors.AsReadOnly();

    public static Bock Create(CreateBockData data)
    {
        // validate 
        Validate(new CreateBockValidator(), data);

        // create
        return new Bock(
            Guid.NewGuid(),
            data.Title,
            data.Description);
    }

    public void Update(UpdateBockData data)
    {
        // validate
        Validate(new UpdateBockValidator(), data);

        // update
        Title = data.Title;
        Description = data.Description;
    }
}