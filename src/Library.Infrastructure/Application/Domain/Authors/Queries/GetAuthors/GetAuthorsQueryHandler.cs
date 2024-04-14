using Library.Application.Domain.Authors.Queries.GetAuthors;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PagesResponses;

namespace Library.Infrastructure.Application.Domain.Authors.Queries.GetAuthors;

public class GetAuthorsQueryHandler(LibrariesDbContext librariesDbContext) : IRequestHandler<GetAuthorsQuery, PageResponse<AuthorDto[]>>
{
    public async Task<PageResponse<AuthorDto[]>> Handle(GetAuthorsQuery query, CancellationToken cancellationToken)
    {
        var sqlQuery  = librariesDbContext
            .Authors
            .AsNoTracking();

        var skipCount = (query.Page - 1) * query.PageSize;
        
        var authors = await sqlQuery
            .Skip(skipCount)
            .Take(query.PageSize)
            .Select(a => new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                MiddleName = a.MiddleName
            })
            .OrderBy(a => a.LastName)
            .ThenBy(a => a.FirstName)
            .ToArrayAsync(cancellationToken);

        var count = await sqlQuery.CountAsync(cancellationToken);

        return new PageResponse<AuthorDto[]>(count, authors);
    }
}