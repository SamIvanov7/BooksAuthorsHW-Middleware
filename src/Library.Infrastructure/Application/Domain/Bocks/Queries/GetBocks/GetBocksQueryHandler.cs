using Library.Application.Domain.Bocks.Queries.GetBocks;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PagesResponses;

namespace Library.Infrastructure.Application.Domain.Bocks.Queries.GetBocks;

public class GetBocksQueryHandler(LibrariesDbContext librariesDbContext) : IRequestHandler<GetBocksQuery, PageResponse<BockDto[]>>
{
    public async Task<PageResponse<BockDto[]>> Handle(GetBocksQuery query, CancellationToken cancellationToken)
    {
        var sqlQuery = librariesDbContext
            .Bocks
            .AsNoTracking();

        var skipCount = (query.Page - 1) * query.PageSize;

        var bocks = await sqlQuery
            .Skip(skipCount)
            .Take(query.PageSize)
            .Select(b => new BockDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description
            })
            .OrderBy(b => b.Title)
            .ToArrayAsync(cancellationToken);

        var count = await sqlQuery.CountAsync(cancellationToken);

        return new PageResponse<BockDto[]>(count, bocks);
    }
}