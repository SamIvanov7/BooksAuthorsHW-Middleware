using Library.Application.Domain.Bocks.Queries.GetBockDetails;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Application.Domain.Bocks.Queries.GetBockDetails;

public class GetBockDetailsQueryHandler(
    LibrariesDbContext librariesDbContext) 
    : IRequestHandler<GetBockDetailsQuery, BockDetailsDto>
{
    public async Task<BockDetailsDto> Handle(GetBockDetailsQuery query, CancellationToken cancellationToken)
    {
        return await librariesDbContext
            .Bocks
            .AsNoTracking()
            .Include(x => x.BocksAuthors)
            .ThenInclude(x => x.Author)
            .Where(x => x.Id == query.Id)
            .Select(x => new BockDetailsDto()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Authors = x.BocksAuthors.Select(ba => new AuthorInformationDto()
                {
                    Id = ba.Author.Id,
                    Name = ba.Author.MiddleName == null
                        ? $"{ba.Author.FirstName} {ba.Author.LastName}"
                        : $"{ba.Author.FirstName} {ba.Author.MiddleName} {ba.Author.LastName}"
                }).ToArray()
            }).SingleAsync(cancellationToken);
    }
}